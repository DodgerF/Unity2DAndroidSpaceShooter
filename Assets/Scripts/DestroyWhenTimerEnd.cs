using UnityEngine;

namespace SpaceShooter
{

    public class DestroyWhenTimerEnd : MonoBehaviour
    {
        [SerializeField] private float m_Time;
        private void Start()
        {
            Destroy(gameObject, m_Time);
        }
    }
}
