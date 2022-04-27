using System;
using UnityEngine;

namespace Scripts
{
    /// <summary>
    /// Brick component spawns an upgrade if it is marked as upgrade
    /// </summary>
    public class Brick : MonoBehaviour
    {
        [SerializeField] private BrickType brickType;
        [SerializeField] private Upgrade upgradePrefab;
        public static int count { private set; get;  }

        private void Start()
        {
            count++;
        }

        private void OnDestroy()
        {
            count--;
            if (brickType == BrickType.Upgrade)
            {
                var upgrade = Instantiate(upgradePrefab, transform.parent);
                upgrade.transform.position = transform.position;
            }
        }
    }
}