using UnityEngine;

namespace SpaceShooter
{
    public class PowerUpWeapon : PickingUp
    {
        [SerializeField] private TurretProperties m_Properties;
        protected override void OnPickedUp(SpaceShip ship)
        { 
            ship.AssignWeapon(m_Properties);
        }
    }

}
