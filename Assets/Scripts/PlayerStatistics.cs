using UnityEngine;

namespace SpaceShooter
{
    public class PlayerStatistics : MonoBehaviour
    {
        private int m_NumKills;
        public int NumKills { get { return m_NumKills; } set { if (value > 0) m_NumKills += value; } }
        private int m_Score;
        public int Score { get { return m_Score; } set { if (value > 0) m_Score += value; } }

        private int m_Time;
        public int Time { get { return m_Time; } set { if (value > 0) m_Time += value; } }


        public void Reset()
        {
            m_NumKills = 0;
            m_Score = 0;
            m_Time = 0;
        }
    }
}
