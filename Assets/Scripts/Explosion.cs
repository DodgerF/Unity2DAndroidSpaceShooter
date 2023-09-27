using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Destructible))]
    public class Explosion : MonoBehaviour
    {
        private Destructible m_Destructable;

        [SerializeField] private float m_ExplosionTime;
        [SerializeField] private GameObject m_PrefExplosion;

        private void Start()
        {
            m_Destructable = GetComponent<Destructible>();
            m_Destructable.EventOnDeth.AddListener(OnExplosion);
        }

        private void OnDisable()
        {
            m_Destructable.EventOnDeth.RemoveListener(OnExplosion);
        }

        private void OnExplosion()
        {
            Instantiate(m_PrefExplosion, m_Destructable.transform.position,Quaternion.identity);
        }
    }
}

