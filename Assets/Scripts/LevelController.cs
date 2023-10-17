using UnityEngine;

namespace SpaceShooter
{
    public interface ILevelCondition
    {
        public bool IsComplited { get; }
    }
    public class LevelController : SingletonBase<LevelController>
    {
        #region Properties
        [SerializeField] private int m_ReferenceTime;
        public int ReferenceTime => m_ReferenceTime;
        [SerializeField] private int BonusScorePerSecond;

        private ILevelCondition[] m_Conditions;
        private bool m_IsComplited;
        
        private float m_LevelTime;
        public float LevelTime => m_LevelTime;
        #endregion

        #region UnityEvents
        private void Start()
        {
            m_Conditions = GetComponentsInChildren<ILevelCondition>();

        }

        private void Update()
        {
            if (m_IsComplited) return;

            m_LevelTime += Time.deltaTime;

            CheckLevelConditions();
        }

        #endregion

        #region Logic
        private void CheckLevelConditions()
        {
            if (m_Conditions == null || m_Conditions.Length == 0) return;

            int numCompleted = 0;

            foreach(var v in m_Conditions)
            {
                if (v.IsComplited)
                {
                    numCompleted++;
                }
            }
            if (numCompleted == m_Conditions.Length)
            {
                m_IsComplited = true;

                if (m_LevelTime < m_ReferenceTime)
                {
                    Player.Instance.AddScore((int)(m_ReferenceTime - m_LevelTime) * BonusScorePerSecond);
                }

                LevelSequenceController.Instance.FinishCurrentLevel(true);
            }
        }
        #endregion
    }
}
