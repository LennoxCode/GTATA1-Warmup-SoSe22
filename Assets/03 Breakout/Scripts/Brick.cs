using System;
using UnityEngine;

namespace Scripts
{
    /// <summary>
    /// Brick component spawns an upgrade if it is marked as upgrade
    /// i decided to use a static variable to keep count of all the bricks
    /// this makes it easy to keep track of them. if i had used a serializable field it would be too level dependent
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