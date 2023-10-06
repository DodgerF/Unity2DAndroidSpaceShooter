using UnityEngine;

namespace SpaceShooter
{
    public class CircleArea : MonoBehaviour
    {
        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        public Vector2 GetRandomInsideZone()
        {
            return (Vector2) transform.position + (Vector2)Random.insideUnitSphere * m_Radius;
        }

#if UNITY_EDITOR
        private static Color GizmoColor = new Color(0, 1, 0, 0.1f);

        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = GizmoColor;
            UnityEditor.Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
#endif
    }
}
