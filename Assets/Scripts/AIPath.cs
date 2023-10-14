using UnityEngine;

namespace SpaceShooter
{
    public class AIPath : MonoBehaviour
    {
        [SerializeField] private AIZonePatrol[] m_Points;
        public int Lenght => m_Points.Length;
        public AIZonePatrol this[int i] => m_Points[i];


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            foreach(var p in m_Points)
            {
                Gizmos.DrawSphere(p.transform.position, p.Radius);
            }
        }
    }
}