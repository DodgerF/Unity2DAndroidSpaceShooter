using UnityEngine;

namespace SpaceShooter
{
    public class Astronaut : PickingUp
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.root.TryGetComponent<GravityWell>(out GravityWell spaceWell))
            {
                Destroy(gameObject);
            }
        }

        protected override void OnPickedUp(SpaceShip ship)
        {
            
        }
    }
}

