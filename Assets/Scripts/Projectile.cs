using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        [SerializeField] protected float m_Velocity;
        [SerializeField] protected float m_Lifetime;
        [SerializeField] protected float m_Damage;
        protected float m_Timer;

        protected Destructible m_Parent;

        #region Unity Events

        protected virtual void Update()
        {
            float stepLenght = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLenght;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);
            if (hit)
            {
                if (hit.collider.transform.root.TryGetComponent<Destructible>(out Destructible destructible) && destructible != m_Parent)
                {
                    destructible.ApplyDamage(m_Damage);
                }
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;
            if (m_Timer > m_Lifetime)
            {
                Destroy(gameObject);
            }

            transform.position += new Vector3(step.x, step.y, 0);
        }

        #endregion

        private void OnProjectileLifeEnd(Collider2D collider, Vector2 position)
        {
            Destroy(gameObject);
        }

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }
    }
}
