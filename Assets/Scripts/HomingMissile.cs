using UnityEngine;

namespace SpaceShooter
{

    public class HomingMissile : Projectile
    {
        [SerializeField] private float m_DistanceToEmptyTarget;
        [SerializeField] private float m_SearchRadius;

        private GameObject m_EmptyTarget;
        private Transform m_Target;

        protected override void Start()
        {
            base.Start();

            m_EmptyTarget = new GameObject("EmptyTarget");
            m_EmptyTarget.transform.position = m_Parent.transform.position + m_Parent.transform.up * m_DistanceToEmptyTarget;

            //Поиск ближайшего разрушаемого объекта в радиусе, который не является родителем снаряда 
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_Parent.transform.position, m_SearchRadius);
            foreach (Collider2D collider in colliders)
            {
                if (collider.transform.root.TryGetComponent<Destructible>(out Destructible target))
                {
                    if (target == m_Parent) continue;

                    if (m_Target == null)
                    {
                        m_Target = collider.transform;
                        continue;
                    }

                    if (Vector2.Distance(m_Parent.transform.position, m_Target.position) >
                        Vector2.Distance(m_Parent.transform.position, collider.transform.position))
                    {
                        m_Target = collider.transform;
                    }
                }
            }
        }

        protected override void Update()
        {
            if (m_Target == null)
            {
                base.Update();
                return;
            }

            float stepLenght = Time.deltaTime * m_Velocity;
            MoveObject(m_EmptyTarget.transform, Direction(m_EmptyTarget.transform, m_Target), stepLenght);
            
            Vector2 missilesDirection = Direction(transform, m_EmptyTarget.transform);

            CheckRaycastAhead(stepLenght, missilesDirection); 
            
            MoveObject(transform, missilesDirection, stepLenght);
            CheckTimer();

        }

        private Vector2 Direction(Transform obj, Transform target) 
        {
            Vector2 direction = target.position - obj.position;
            direction.Normalize();
            return direction;
        }

        private void MoveObject(Transform obj, Vector2 direction, float stepLenght)
        {
            Vector2 step = direction * stepLenght;
            obj.position += new Vector3(step.x, step.y, 0);
        }

        private void CheckRaycastAhead(float stepLenght, Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, stepLenght);
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

        protected override void OnProjectileLifeEnd()
        {
            Destroy(m_EmptyTarget);
            base.OnProjectileLifeEnd();
        }
    }
}
