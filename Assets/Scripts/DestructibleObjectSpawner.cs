using UnityEngine;

namespace SpaceShooter 
{
    [RequireComponent(typeof(CircleArea))]
    public class DestructibleObjectSpawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        [SerializeField] private Destructible[] m_DestructiblePrefabs;
        [SerializeField] private SpawnMode m_SpawnMode; 
        [SerializeField] private int m_NumSpawns;
        [SerializeField] private float m_ÑoefficientSpeed;
        private CircleArea m_Area;

        private void Awake()
        {
            m_Area = GetComponent<CircleArea>();
        }

        private void Start()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                SpawnDestructible();
            }
        }


        private void SpawnDestructible()
        {
            int index = Random.Range(0, m_DestructiblePrefabs.Length);

            GameObject destructible = Instantiate(m_DestructiblePrefabs[index].gameObject);
            destructible.transform.position = m_Area.GetRandomInsideZone();

            destructible.GetComponent<Destructible>().EventOnDeath.AddListener(OnDead);

            if (destructible.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody) && m_ÑoefficientSpeed > 0)
            {
                rigidbody.velocity = (Vector2) Random.insideUnitCircle * m_ÑoefficientSpeed;
            }
        }

        private void OnDead()
        {
            if (m_SpawnMode == SpawnMode.Loop)
            {
                SpawnDestructible();
            }
        }
    }
}
