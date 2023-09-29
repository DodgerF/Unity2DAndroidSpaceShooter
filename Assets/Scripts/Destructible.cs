using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// A destroyable object on the stage that can have hit points
    /// </summary>
    public class Destructible : Entity
    {
        #region Properties

        /// <summary>
        /// Object is ignores damage
        /// </summary>
        [SerializeField] private bool indestructible;
        public bool IsIndestructible => indestructible;

        /// <summary>
        /// Initial hit points
        /// </summary>
        [SerializeField] private int hitPoints;

        /// <summary>
        /// Current hit points
        /// </summary>
        private int currentHitPoints;
        public int HitPoints => currentHitPoints;

        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            currentHitPoints = hitPoints;
        }

        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        #endregion

        #region Public API
        /// <summary>
        /// Applying damage to an object
        /// </summary>
        /// <param name="damage"> Damage dealt to object </param>
        public void ApplyDamage(int damage)
        {

            if (indestructible) return;

            currentHitPoints -= damage;
           
            if (currentHitPoints <= 0)
            {
                OnDeath();
            }
        }

        #endregion

        /// <summary>
        /// Overridden object destruction event when hit points are less than 0
        /// </summary>
        protected virtual void OnDeath()
        {
            m_EventOnDeath?.Invoke();

            Destroy(gameObject);
        }

        
    }

}
