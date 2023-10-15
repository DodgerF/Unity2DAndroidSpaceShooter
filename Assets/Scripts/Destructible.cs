using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// A destroyable object on the stage that can have hit points
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
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
        public float MaxHP => hitPoints;

        /// <summary>
        /// Current hit points
        /// </summary>
        private float currentHitPoints;
        public float HitPoints => currentHitPoints;

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
        public virtual void ApplyDamage(float damage)
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
        private static HashSet<Destructible> m_AllDestructibles;
        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
            {
                m_AllDestructibles = new HashSet<Destructible>();
            }
            m_AllDestructibles.Add(this);
        }

        protected void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        public const int TeamIdNeutral = 0;
        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;


        #region Score

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;

        #endregion

    }

}
