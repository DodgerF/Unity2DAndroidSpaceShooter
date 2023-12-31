using UnityEngine;

namespace SpaceShooter
{
    public class MainMenuController : SingletonBase<MainMenuController>
    {
        [SerializeField] private GameObject m_EpisodeSelection;

        [SerializeField] private SpaceShip m_DefaultSpaceShip;
        [SerializeField] private GameObject m_ShipSelection;
        [SerializeField] private GameObject m_ResultSelection;


        protected override void Awake()
        {
            base.Awake();

            gameObject.SetActive(true);
            m_EpisodeSelection.gameObject.SetActive(false);
            m_ShipSelection.gameObject.SetActive(false);

        }

        private void Start()
        {
            LevelSequenceController.PlayerShip = m_DefaultSpaceShip;
        }

        public void OnButtonStart()
        {
            m_EpisodeSelection.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnSelectShip()
        {
            m_ShipSelection.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnSelectResult()
        {
            if (m_ResultSelection.activeSelf) 
            { 
                m_ResultSelection.SetActive(false);
            }
            else
            {
                m_ResultSelection.SetActive(true);
            }
        }

        public void OnButtonEnd()
        {
            Application.Quit();
        }
    }
}
