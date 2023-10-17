using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CameraController))]
    [RequireComponent (typeof(MovementController))]
    public class Player : SingletonBase<Player>
    {
        #region Properties
        [SerializeField] private int m_NumLives;
        [SerializeField] private SpaceShip m_Ship;
        public SpaceShip Ship => m_Ship;

        private CameraController m_CameraController;
        private MovementController m_MovementController;

        #endregion

        #region UnityEvents
        protected override void Awake()
        {
            base.Awake();

            m_CameraController = GetComponent<CameraController>();
            m_MovementController = GetComponent<MovementController>();
            
            if (m_Ship != null && LevelSequenceController.PlayerShip != null)
            {
                Destroy(m_Ship.gameObject);
                Respawn();
            }
        }

        private void MyOnEnable()
        {
            m_Ship.EventOnDeath.AddListener(OnShopDeath);
        }

        private void OnDisable()
        {
            m_Ship.EventOnDeath.RemoveListener(OnShopDeath);
        }
        #endregion
        private void OnShopDeath()
        {
            m_NumLives--;

            if (m_NumLives > 0)
            {
                Respawn();
            }
            else
            {
                LevelSequenceController.Instance.FinishCurrentLevel(false);
            }
        }

        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip == null) return;

            var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

            OnDisable();
            m_Ship = newPlayerShip.GetComponent<SpaceShip>();
            MyOnEnable();

            m_CameraController.SetTarget(m_Ship.transform);
            m_MovementController.SetTargetShip(m_Ship);

        }
        #region Score
        public int Score { get; private set; }
        public int NumKills { get; private set; }

        public void AddKill()
        {
            NumKills++;
        }
        public void AddScore(int num)
        {
            if (num  < 0)
            {
                Debug.Log("не может быть добавленно отрицательное число.");
                return;
            }
            Score += num;
        }
        #endregion
    }
}