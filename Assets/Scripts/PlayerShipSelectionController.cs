using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    [RequireComponent(typeof(Image))]
    public class PlayerShipSelectionController : MonoBehaviour
    {
        [SerializeField] private SpaceShip m_Prefab;

        [SerializeField] private TextMeshProUGUI m_Shipname;
        [SerializeField] private TextMeshProUGUI m_Hitpoints;
        [SerializeField] private TextMeshProUGUI m_Speed;
        [SerializeField] private TextMeshProUGUI m_Agility;
        [SerializeField] private GameObject m_ShipSelectPanel;

        private Image m_Preview;

        private void Awake()
        {
            m_Preview = GetComponent<Image>();
        }

        private void Start()
        {
            if (m_Prefab == null) return;
            
            m_Shipname.text = m_Prefab.Nickname;
            m_Hitpoints.text = "Hp: " + m_Prefab.MaxHP;
            m_Speed.text = "Speed: " + m_Prefab.MaxLinearVelocity;
            m_Agility.text = "Agility: " + m_Prefab.MaxAngularVelocity;
            m_Preview.sprite = m_Prefab.PreviewImage.sprite;
        }

        public void OnSelectShip()
        {
            LevelSequenceController.PlayerShip = m_Prefab;
            MainMenuController.Instance.gameObject.SetActive(true);
            m_ShipSelectPanel.SetActive(false);
        }


    }
}