using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    public class GameStatisticsPanelController : ResultPanelController
    {
        [SerializeField] private TextMeshProUGUI m_AstronautsSavedText;

        public void ShowResults()
        {
            
            m_NumKillsText.text = "Kills: " + GameStatistics.Instance.NumKills;
            m_ScoreText.text = "Score: " + GameStatistics.Instance.Score;
            m_TimeText.text = "Time: " + GameStatistics.Instance.Time;
            m_AstronautsSavedText.text = "AstronautsSaved: " + GameStatistics.Instance.AstronautsSaved;
        }
    }
}
