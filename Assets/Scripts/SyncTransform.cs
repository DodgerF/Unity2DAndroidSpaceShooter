using UnityEngine;

namespace SpaceShooter
{/// <summary>
/// Скрипт, который двигает фон вслед за игроком по X и Y.
/// </summary>
    public class SyncTransform : MonoBehaviour
    {
        [SerializeField] private Transform m_Target;
        void Update()
        {
            transform.position = new Vector3(m_Target.position.x, m_Target.position.y, transform.position.z);
        }
    }
}
