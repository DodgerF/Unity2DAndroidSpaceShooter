using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Animator))]
    public class DestroyerOnTimer : MonoBehaviour
    {
        [SerializeField] private float m_Time;
        private void Start()
        {
            Destroy(gameObject, m_Time);
        }
    }
}
