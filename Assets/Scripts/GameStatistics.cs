namespace SpaceShooter
{
    public class GameStatistics : SingletonBase<GameStatistics>
    {
        private int m_NumKills;
        public int NumKills => m_NumKills;

        private int m_Score;
        public int Score => m_Score;

        private int m_Time;
        public int Time => m_Time;

        private int m_AstronautsSaved;
        public int AstronautsSaved => m_AstronautsSaved;

        protected override void Awake()
        {
            m_NumKills = 0;
            m_Score = 0;
            m_Time = 0;
            m_AstronautsSaved = 0;

            base.Awake();
        }

        public void AddKills(int value)
        {
            if (value <= 0) return;

            m_NumKills += value;
        }
        public void AddScore(int value)
        {
            if (value <= 0) return;

            m_Score += value;
        }
        public void AddTime(int value)
        {
            if (value <= 0) return;

            m_Time += value;
        }
        public void AddAstronauts(int value)
        {
            if (value <= 0) return;

            m_AstronautsSaved += value;
        }
    }
}
