using UnityEngine;


namespace SpaceShooter
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties;

        private float m_RefireTimer;

        public bool IsCanFire => m_RefireTimer <= 0;

        private SpaceShip m_Ship;

        #region UnityEvents
        private void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>();
            if (m_Ship == null)
            {
                Debug.LogWarning("SpaceShip in Turret class not found! The turret must be on a GameObject with the SpaceShip class.");
                return;
            }
        }
        private void Update()
        {
            if (m_RefireTimer > 0)
            {
                m_RefireTimer -= Time.deltaTime;
            }
        }
        #endregion

        #region Public API
        public void Fire()
        {
            if (m_TurretProperties == null) return;
            if (m_RefireTimer > 0) return;

            if (m_Ship.DrawEnergy(m_TurretProperties.EnergeUsage) == false) return;

            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false) return;

            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;
            projectile.SetParentShooter(m_Ship);

            m_RefireTimer = m_TurretProperties.RateOfFire;

            {
                //SFX
            }
        }
        public void AssignLoadout(TurretProperties properties)
        {
            if (m_Mode != properties.Mode) return;

            m_RefireTimer = 0;
            m_TurretProperties = properties;
        }

        #endregion
    }
}
