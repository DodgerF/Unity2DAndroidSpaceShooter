using TMPro;
using UnityEngine;


namespace SpaceShooter
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UIScore : MonoBehaviour
    {
        private TextMeshProUGUI m_Text;
        private int m_LastScore;

        private void Awake()
        {
            m_Text = GetComponent<TextMeshProUGUI>();
            m_LastScore = 0; 
        }

        private void Update()
        {
            UpdateScore();
        }

        private void UpdateScore()
        {
            if (Player.Instance == null) return;

            int currentScore = Player.Instance.Score;

            if (m_LastScore != currentScore)
            {
                m_LastScore = currentScore;
                m_Text.text = "Score: " + m_LastScore;
            }

        }
    }
}
