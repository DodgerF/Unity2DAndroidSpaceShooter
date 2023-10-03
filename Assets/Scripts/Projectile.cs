using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] private float m_Velocity;
        [SerializeField] private float m_Lifetime;
        [SerializeField] private float m_Damage;
        [SerializeField] private DestroyWhenTimerEnd m_DestroyTimerPref;
        private float m_Timer;

        #region Unity Events

        private void Update()
        {
            
        }

        #endregion
    }
}
