using UnityEngine;

namespace SpaceShooter
{
    public class AIPath : MonoBehaviour
    {
        [SerializeField] private Area[] m_Points;
        public int Lenght => m_Points.Length;
        public Area this[int i] => m_Points[i];

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            foreach(var p in m_Points)
            {
                Gizmos.DrawSphere(p.transform.position, p.Radius);
            }
        }
#endif
    }
}