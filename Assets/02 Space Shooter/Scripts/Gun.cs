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

        private void TripleFire()
        {
            laserPrefab.initialVelocity = ship.movementObject.CurrentVelocity;
            Instantiate(laserPrefab, transform.position, transform.rotation);
            Instantiate(laserPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 33));
            Instantiate(laserPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, -33));
        }
    }
}