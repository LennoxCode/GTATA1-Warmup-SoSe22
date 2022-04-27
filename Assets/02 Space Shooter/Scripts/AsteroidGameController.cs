using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Scripts
{
    /// <summary>
    /// Game controller handling asteroids and intersection of components.
    /// </summary>
    public class AsteroidGameController : MonoBehaviour
    {
        public Asteroid[] bigAsteroids;
        public Asteroid[] mediumAsteroids;
        public Asteroid[] smallAsteroids;
        public PowerUp[] powerUps;
        public bool isGameActive { get; private set; } 
        public Action<Effect> activatedPowerUp;
        
        [SerializeField] private Vector3 maximumSpeed, maximumSpin;
        [SerializeField] private PlayerShip playerShip;
        [SerializeField] private Transform spawnAnchor;
        [SerializeField] private int maximumPowerUPs;
        private List<Asteroid> activeAsteroids;
        private Random random;
        private List<PowerUp> activePowerUps;
        private float lastPlayerHit = 0;
        private void Awake()
        {
           
        }

        private void Start()
        {
            MenuController.instance.onRestart += RestartGame;
            activeAsteroids = new List<Asteroid>();
            activePowerUps = new List<PowerUp>();
            random = new Random();
            // spawn some initial asteroids
            for (var i = 0; i < 5; i++)
            {
                SpawnAsteroid(bigAsteroids, Camera.main.OrthographicBounds());
            }
            //SpawnPowerUp(powerUps, Camera.main.OrthographicBounds());
            activatedPowerUp += ActivatePowerUP;
            StartCoroutine(SpawnPowerUp(powerUps, Camera.main.OrthographicBounds()));
            isGameActive = true;
        }

        void Update()
        {
            if (activeAsteroids.Count == 0)
            {
                MenuController.instance.ShowVictoryScreen();
                isGameActive = false;
            }
        }
        /// <summary>
        /// Behaviour to spawn an asteroid within the screen
        /// If there is a parent given, the velocity of that parent is put into consideration
        /// </summary>
        private void SpawnAsteroid(Asteroid[] prefabs, Bounds inLocation, Asteroid parent = null)
        {
            // get a random prefab from the list
            var prefab = prefabs[random.Next(prefabs.Length)];
            // create an instance of the prefab
            var newObject = Instantiate(prefab, spawnAnchor);
            // position it randomly within the box given (either the parent asteroid or the camera)
            newObject.transform.position = RandomPointInBounds(inLocation);
            // we can randomly invert the x/y scale to mirror the sprite. This creates overall more variety
            newObject.transform.localScale = new Vector3(UnityEngine.Random.value > 0.5f ? -1 : 1,
                UnityEngine.Random.value > 0.5f ? -1 : 1, 1);
            // renaming, I'm also sometimes lazy typing
            var asteroidSprite = newObject.spriteRenderer;

            // try to position the asteroid somewhere where it doesn't hit the player or another active asteroid
            for (var i = 0;
                playerShip.shipSprite.bounds.Intersects(asteroidSprite.bounds) ||
                activeAsteroids.Any(x => x.GetComponent<SpriteRenderer>().bounds.Intersects(asteroidSprite.bounds));
                i++)
            {
                // give up after 15 tries.
                if (i > 15)
                {
                    DestroyImmediate(newObject.gameObject);
                    return;
                }

                newObject.transform.position = RandomPointInBounds(inLocation);
            }
            
            // take parent velocity into consideration
            if (parent != null)
            {
                var offset = parent.transform.position - newObject.transform.position;
                var parentVelocity = parent.movementObject.CurrentVelocity.magnitude *
                                     (UnityEngine.Random.value * 0.4f + 0.8f);
                newObject.movementObject.Impulse(offset.normalized * parentVelocity, RandomizeVector(maximumSpeed));
            }
            // otherwise randomize just some velocity
            else
            {
                newObject.movementObject.Impulse(RandomizeVector(maximumSpeed), RandomizeVector(maximumSpin));
            }

            activeAsteroids.Add(newObject);
        }

        private IEnumerator SpawnPowerUp(PowerUp[] prefabs, Bounds inLocation)
        {
            while (true)
            {
                if(activePowerUps.Count < maximumPowerUPs && isGameActive){
                    var prefab = prefabs[random.Next(prefabs.Length)];
                    // create an instance of the prefab
                    var newObject = Instantiate(prefab, spawnAnchor);
                    // position it randomly within the box given (either the parent asteroid or the camera)
                    newObject.transform.position = RandomPointInBounds(inLocation);
                    newObject.movementObject.Impulse(RandomizeVector(maximumSpeed / 3), new Vector3(0,0,0));
                    activePowerUps.Add(newObject);
                }
                yield return new WaitForSeconds(5f);
            }
        }
        private void RestartGame()
        {
            foreach (var activeAsteroid in activeAsteroids)
            { 
                Destroy(activeAsteroid.gameObject);
                
            }

            foreach (PowerUp powerUP in activePowerUps)
            {
                Destroy(powerUP.gameObject);
            }
            activeAsteroids.Clear();
            activePowerUps.Clear();
            PlayerStats.instance.ResetHealth();
            random = new Random();
            for (var i = 0; i < 5; i++)
            {
                SpawnAsteroid(bigAsteroids, Camera.main.OrthographicBounds());
            }
            isGameActive = true;
        }
        /// <summary>
        /// Checks if a laser is intersecting with an asteroid and executes gameplay behaviour on that
        /// </summary>
        public void LaserIntersection(SpriteRenderer laser)
        {
            // go through all asteroids, check if they intersect with a laser and stop after the first
            var asteroid = activeAsteroids
                .FirstOrDefault(x => x.GetComponent<SpriteRenderer>().bounds.Intersects(laser.bounds));

            // premature exit: this laser hasn't hit anything
            if (asteroid == null)
            {
                return;
            }
            
            // otherwise remove the asteroid from the tracked asteroid
            activeAsteroids.Remove(asteroid);
            var bounds = asteroid.spriteRenderer.bounds;
            // get the correct set of prefabs to spawn asteroids in place of the asteroid that now explodes
            var prefabs = asteroid.asteroidSize switch
            {
                AsteroidSize.Large => mediumAsteroids,
                AsteroidSize.Medium => smallAsteroids,
                _ => null
            };
            // remote the asteroid gameobject with all its components
            Destroy(asteroid.gameObject);
            // premature exit: we have no prefabs (ie: small asteroids exploding)
            if (prefabs == null)
            {
                return;
            }

            // randomize two to six random asteroids
            var objectCountToSpawn = (int) (UnityEngine.Random.value * 4 + 2);
            for (var i = 0; i < objectCountToSpawn; i++)
            {
                SpawnAsteroid(prefabs, bounds);
            }
        
            // oh, also get rid of the laser now
            Destroy(laser.gameObject);
        }

        public void ShipIntersection(SpriteRenderer ship)
        {
            if (Time.time - lastPlayerHit < 1.5f) return;
            var asteroid = activeAsteroids
                .FirstOrDefault(x => x.GetComponent<SpriteRenderer>().bounds.Intersects(playerShip.shipSprite.bounds));
            if (asteroid == null) return;
            lastPlayerHit = Time.time;
            PlayerStats.instance.health--;
            if (PlayerStats.instance.health == 0)
            {
                MenuController.instance.ShowLooseScreen();
                isGameActive = false;
            }
            
        }

        public void PowerUpIntersection(SpriteRenderer ship)
        {
            var powerUP = activePowerUps
                .FirstOrDefault(x => x.GetComponent<SpriteRenderer>().bounds.Intersects(playerShip.shipSprite.bounds));
            if (powerUP == null) return;
            activePowerUps.Remove(powerUP);
            Destroy(powerUP.gameObject);
            activatedPowerUp.Invoke(powerUP.effect);

        }

        private void ActivatePowerUP(Effect effect)
        {
            if(effect != Effect.SlowMotion) return;
            Time.timeScale = 0.5f;
            Invoke("DeactiveatePowerUp", PowerUp.duration);
            
        }

        private void DeactiveatePowerUp()
        {
            Time.timeScale = 1f;
        }
        private static float RandomPointOnLine(float min, float max)
        {
            return UnityEngine.Random.value * (max - min) + min;
        }

        private static Vector2 RandomPointInBox(Vector2 min, Vector2 max)
        {
            return new Vector2(RandomPointOnLine(min.x, max.x), RandomPointOnLine(min.y, max.y));
        }

        private static Vector2 RandomPointInBounds(Bounds bounds)
        {
            return RandomPointInBox(bounds.min, bounds.max);
        }

        private static Vector3 RandomizeVector(Vector3 maximum)
        {
            // that is an inline method - it's good enough to just get a float [-1...+1]
            float RandomValue()
            {
                return UnityEngine.Random.value - 0.5f * 2;
            }

            maximum.Scale(new Vector3(RandomValue(), RandomValue(), RandomValue()));
            return maximum;
        }
    }
}