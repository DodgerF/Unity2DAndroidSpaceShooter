using UnityEngine;

namespace SpaceShooter
{
    public class PowerUpSpeed : PickingUp
    {
        [SerializeField] private float m_SpeedBonus;
        [SerializeField] private float m_Time;
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.AddSpeed(m_SpeedBonus, m_Time);
        }
    }
}