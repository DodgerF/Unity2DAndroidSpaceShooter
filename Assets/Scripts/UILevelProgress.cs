using TMPro;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UILevelProgress : MonoBehaviour
    {
        [SerializeField] private LevelConditionAstronuats m_Astronauts;
        private TextMeshProUGUI m_LevelProgress;

        private void Awake()
        {
            m_LevelProgress = GetComponent<TextMeshProUGUI>();
            RefreshText();
        }

        private void Update()
        {
            RefreshText();
        }

        private void RefreshText()
        {
            m_LevelProgress.text = AstronautsCounter.Instance.Saved + "/" + AstronautsCounter.Instance.Amount;
        }
    }

}
