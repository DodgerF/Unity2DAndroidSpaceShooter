using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionScore : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private int score;
        private bool m_Reached;
        bool ILevelCondition.IsComplited
        {
            get
            {
                if (Player.Instance != null && Player.Instance.Ship != null)
                {
                    if (Player.Instance.Score >= score)
                    {
                        m_Reached = true;
                    }
                }
                return m_Reached;
            }
        }
    }
}
