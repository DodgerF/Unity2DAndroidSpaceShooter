using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Astronaut : PickingUp
    {  
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.root.TryGetComponent<SpaceShip>(out SpaceShip col))
            {
                return;
            }
            AstronautsCounter.Instance.ReduceAmount(1);

            Destroy(gameObject);
        }

        protected override void OnPickedUp(SpaceShip ship)
        {
            AstronautsCounter.Instance.InreaseSaved(1);
        }
    }
}

