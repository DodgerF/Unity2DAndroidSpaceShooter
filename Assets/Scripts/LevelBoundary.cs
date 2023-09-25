using UnityEngine;

namespace SpaceShooter
{
    public class LevelBoundary : MonoBehaviour
    {

        #region Singleton
        public static LevelBoundary Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("LevelBoundary already exist");
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        #endregion

        [SerializeField] private float m_Radius;
        public float Radius => m_Radius;

        public enum Mode
        {
            Limit,
            Teleport
        }

        [SerializeField] private Mode m_BoundaryMode;
        public Mode BoundaryMode => m_BoundaryMode;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
        }
#endif

    }
}

