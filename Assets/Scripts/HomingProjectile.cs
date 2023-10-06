using UnityEngine;

namespace SpaceShooter
{
    public class HomingProjectile : Projectile
    {
        [SerializeField] private float m_SearchRadius;

        [Range(0f, 360f)]
        [SerializeField] private float m_SearchAngle;

        private Transform m_Target;

        private void Awake()
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, Vector2.one * m_SearchRadius, m_SearchAngle);
             
            foreach(Collider2D collider in colliders)
            {

                if (collider.transform.root.TryGetComponent<Destructible>(out Destructible target) && target != m_Parent)
                {
                    if (m_Target == null)
                    {
                        m_Target = collider.transform;
                        continue;
                    }

                    if (Vector2.Distance(transform.position, m_Target.position) > Vector2.Distance(transform.position, collider.transform.position))
                    {
                        m_Target = collider.transform;
                    }
                }
            }

        }
        protected override void Update()
        {
            /*if (m_Target == null)
            {
                base.Update();
                return;
            }

            float stepLenght = Time.deltaTime * m_Velocity;
            Vector2 step = m_Target.position - transform.position;
            step.Normalize();

            float rotateAmount = Vector3.Cross(d)*/
        }
    }

}
