using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        #region Properties
        [SerializeField] protected float m_Velocity;
        public float Velocity => m_Velocity;
        [SerializeField] protected float m_Lifetime;
        [SerializeField] protected float m_Damage;
        protected float m_Timer;

        protected Destructible m_Parent;
        protected bool isPlayerShot;
        #endregion

        #region Unity Events
        protected virtual void Start()
        {
            if (m_Parent == Player.Instance.Ship)
            {
                isPlayerShot = true;
            }
        }

        protected virtual void Update()
        {
            float stepLenght = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLenght;

            CheckRaycastAhead(stepLenght);
            CheckTimer();

            transform.position += new Vector3(step.x, step.y, 0);
        }

        #endregion

        private void CheckRaycastAhead(float stepLenght)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);
            if (hit)
            {
                if (hit.collider.transform.TryGetComponent<Destructible>(out Destructible destructible) && destructible != m_Parent)
                {
                    destructible.ApplyDamage(m_Damage);

                    UpdateScore(destructible);
                }
                OnProjectileLifeEnd();
            }
        }

        protected void UpdateScore(Destructible destructible)
        {
            if (isPlayerShot)
            {
                Player.Instance.AddScore(destructible.ScoreValue);

                if (destructible.TryGetComponent<SpaceShip>(out SpaceShip ship))
                {
                    if (ship.HitPoints <= 0)
                    {
                        Player.Instance.AddKill();
                    }
                }
            }
        }
        protected void CheckTimer()
        {
            m_Timer += Time.deltaTime;
            if (m_Timer > m_Lifetime)
            {
                OnProjectileLifeEnd();
            }
        }

        protected virtual void OnProjectileLifeEnd()
        {
            Destroy(gameObject);
        }

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }
    }
}
