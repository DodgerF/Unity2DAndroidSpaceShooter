namespace SpaceShooter
{

    public class AstronautsCounter : SingletonBase<AstronautsCounter>
    {
        private int m_AmountAstronauts;
        public int Amount => m_AmountAstronauts;

        private int m_SavedAstronauts;
        public int Saved => m_SavedAstronauts;


        protected override void Awake()
        {
            base.Awake();

            m_SavedAstronauts = 0;
            m_AmountAstronauts = FindObjectsOfType(typeof(Astronaut)).Length;
        }

        public void ReduceAmount(int amount)
        {
            if (amount < 0 || amount > m_AmountAstronauts) return;

            m_AmountAstronauts -= amount;
        }
        public void InreaseSaved(int amount)
        {
            if (amount < 0 || m_AmountAstronauts - m_SavedAstronauts < amount) return;

            m_SavedAstronauts += amount;
        }
    }
}