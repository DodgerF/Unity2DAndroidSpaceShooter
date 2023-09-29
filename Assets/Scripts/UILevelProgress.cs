using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UILevelProgress : MonoBehaviour
    {
        private TextMeshProUGUI m_LevelProgress;

        private int m_AstronautsOnMap;
        private int m_AstronautsSaved;

        private void Awake()
        {
            m_AstronautsSaved = 0;
            m_AstronautsOnMap = FindObjectsOfType(typeof(Astronaut)).Length;

            m_LevelProgress = GetComponent<TextMeshProUGUI>();
            RefreshText();
        }

        private void OnEnable()
        {
            GlobalEventManager.Instance.AstonautOnDeath.AddListener(OnDeath);
            GlobalEventManager.Instance.AstonautOnPicked.AddListener(OnPicked);
        }
        private void OnDisable()
        {
            GlobalEventManager.Instance.AstonautOnDeath.RemoveListener(OnDeath);
            GlobalEventManager.Instance.AstonautOnPicked.RemoveListener(OnPicked);
        }

        private void OnPicked()
        {
            m_AstronautsSaved++;
            RefreshText();
        }
        private void OnDeath()
        {
            m_AstronautsOnMap--;
            RefreshText();
        }
        private void RefreshText()
        {
            m_LevelProgress.text = m_AstronautsSaved + "/" + m_AstronautsOnMap;
        }
    }

}
