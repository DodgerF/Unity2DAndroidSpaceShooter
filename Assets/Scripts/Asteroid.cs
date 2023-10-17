using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Explosion))]
    public class Asteroid : Destructible
    {
        [SerializeField] private Asteroid m_AsteroidPrefab;
        [SerializeField] private float m_ScaleDivider;
        [SerializeField] private float m_ScatteringRadiusOfRemains;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.root.TryGetComponent<GravityWell>(out GravityWell gravityWell))
            {
                OnDeath();
            }           
        }

        protected override void OnDeath()
        {
            if (transform.localScale == Vector3.one)
            {
                for (int i = 0; i < 2; i++)
                { 
                    Rigidbody2D rigid = Instantiate(m_AsteroidPrefab,
                        new Vector3(
                        transform.position.x + m_ScatteringRadiusOfRemains,
                        transform.position.y + m_ScatteringRadiusOfRemains, 
                        transform.position.z),
                        Quaternion.identity).GetComponent<Rigidbody2D>();

                    rigid.gameObject.transform.localScale = transform.localScale / m_ScaleDivider;

                    rigid.velocity = Vector2.one * m_ScatteringRadiusOfRemains;

                    rigid.gameObject.GetComponent<Explosion>().Scale /= m_ScaleDivider;
                    m_ScatteringRadiusOfRemains *= -1;
                }
            }
            base.OnDeath();
        }
    }
}
