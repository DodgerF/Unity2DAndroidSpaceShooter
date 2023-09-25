using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CameraController))]
    [RequireComponent (typeof(MovementController))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private int m_NumLives;
        [SerializeField] private SpaceShip m_Ship;
        [SerializeField] private GameObject m_PlayerShipPref;

        private CameraController m_CameraController;
        private MovementController m_MovementController;

        private void Start()
        {
            m_CameraController = GetComponent<CameraController>();
            m_MovementController = GetComponent<MovementController>();
        }

        private void OnEnable()
        {
            m_Ship.EventOnDeth.AddListener(OnShopDeath);
        }

        private void OnDisable()
        {
            m_Ship.EventOnDeth.RemoveListener(OnShopDeath);
        }

        private void OnShopDeath()
        {
            m_NumLives--;

            if (m_NumLives > 0)
            {
                Respawn();
            }
        }

        private void Respawn()
        {
            var newPlayerShip = Instantiate(m_PlayerShipPref);

            OnDisable();
            m_Ship = newPlayerShip.GetComponent<SpaceShip>();
            OnEnable();

            m_CameraController.SetTarget(m_Ship.transform);
            m_MovementController.SetTargetShip(m_Ship);

        }
    }
}