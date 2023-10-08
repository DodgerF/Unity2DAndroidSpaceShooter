using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Destructible))]
    public class Explosion : MonoBehaviour
    {
        private Destructible m_Destructable;

        [SerializeField] private float m_ExplosionTime;
        [SerializeField] private GameObject m_PrefExplosion;
        [SerializeField] public float Scale;

        private void Start()
        {
            m_Destructable = GetComponent<Destructible>();
            m_Destructable.EventOnDeath.AddListener(OnExplosion);
        }

        private void OnDisable()
        {
            m_Destructable.EventOnDeath.RemoveListener(OnExplosion);
        }

        private void OnExplosion()
        {
            GameObject explosion = Instantiate(m_PrefExplosion, m_Destructable.transform.position, Quaternion.identity);
            explosion.transform.localScale = Vector3.one * Scale;
        }
    }
}

