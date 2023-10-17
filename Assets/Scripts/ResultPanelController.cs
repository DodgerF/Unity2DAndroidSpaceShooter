using System;
using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        #region Properties
        [SerializeField] protected TextMeshProUGUI m_ResultText;


        [SerializeField] protected TextMeshProUGUI m_NumKillsText;
        [SerializeField] protected TextMeshProUGUI m_ScoreText;
        [SerializeField] protected TextMeshProUGUI m_TimeText;

        [SerializeField] private TextMeshProUGUI m_ButtonText;

        private bool m_Success;
        #endregion

        #region Unity Events
        protected override void Awake()
        {
            base.Awake();
            gameObject.SetActive(false);
        }

        
        #endregion

        #region API
        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;

            m_Success = success;
            m_ResultText.text = success ? "Win" : "Lose";
            m_ButtonText.text = success ? "Next" : "Restart";

            m_NumKillsText.text = "Kills: " + levelResults.NumKills;
            m_ScoreText.text = "Score: " + levelResults.Score;
            m_TimeText.text = "Time: " + levelResults.Time;
        }

        public void OnButtonNext()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;

            if (m_Success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                gameObject.SetActive(false);
                LevelSequenceController.Instance.RestartLevel();
            }

        }
        #endregion
    }
}