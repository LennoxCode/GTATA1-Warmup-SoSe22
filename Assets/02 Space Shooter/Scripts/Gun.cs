using System.Linq.Expressions;
using UnityEngine;

namespace Scripts
{
    /// <summary>
    /// Simple component to create a laser and shoot it forward 
    /// </summary>
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Laser laserPrefab;
        private PlayerShip ship;
        private bool hasTripleShot = false;
        private static AsteroidGameController _runGameController;
        private void Start()
        {
            ship = GetComponent<PlayerShip>();
            _runGameController = FindObjectOfType<AsteroidGameController>();
            _runGameController.activatedPowerUp += ActivatePowerUP;
        }

        private void Update()
        {
            if (!_runGameController.isGameActive) return;
            if (Input.GetKeyDown(KeyCode.Space))
            {
               if(hasTripleShot) TripleFire();
               else Fire();
            }
        }
        /// <summary>
        /// same logic for the powerUP: it is subscribed to the related event and applies its effect
        /// </summary>
        private void ActivatePowerUP(Effect effect)
        {
            if (effect != Effect.TripleShot) return;
            hasTripleShot = true;
            Invoke("DeactivatePowerUP", PowerUp.duration);

        }

        private void DeactivatePowerUP()
        {
            hasTripleShot = false;
        }
        private void Fire()
        {
            laserPrefab.initialVelocity = ship.movementObject.CurrentVelocity;
            Instantiate(laserPrefab, transform.position, transform.rotation);
        }
        /// <summary>
        /// this function is called as an alternative to the normal fire
        /// it fires three projectiles instead of one each angled 33 degrees
        /// </summary>
        private void TripleFire()
        {
            laserPrefab.initialVelocity = ship.movementObject.CurrentVelocity;
            Instantiate(laserPrefab, transform.position, transform.rotation);
            Instantiate(laserPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 33));
            Instantiate(laserPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, -33));
        }
    }
}