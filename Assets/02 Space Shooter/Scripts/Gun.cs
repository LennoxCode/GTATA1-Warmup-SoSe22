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
        private LambdaExpression currentShooter;
        private void Start()
        {
            ship = GetComponent<PlayerShip>();
           // currentShooter = Fire();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TripleShot();
            }
        }

        private void Fire()
        {
            laserPrefab.initialVelocity = ship.movementObject.CurrentVelocity;
            Instantiate(laserPrefab, transform.position, transform.rotation);
        }

        private void TripleShot()
        {
            laserPrefab.initialVelocity = ship.movementObject.CurrentVelocity;
            Instantiate(laserPrefab, transform.position, transform.rotation);
            Instantiate(laserPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 45));
            Instantiate(laserPrefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, -45));
        }
    }
}