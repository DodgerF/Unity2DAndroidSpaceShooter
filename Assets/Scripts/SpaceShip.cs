using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// ����� ��� �������������� ��������� � ������.
        /// </summary>
        [Header("SpaceShip")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// ��������� ������ ����.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// ��������� ����.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// ������������ �������� ��������.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;

        /// <summary>
        /// ������������ ������������ �������� � ��������/���.
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;

        /// <summary>
        /// ������ ��������
        /// </summary>
        [SerializeField] private Turret[] m_Turrets;

        /// <summary>
        /// ����������� ������ �� �����
        /// </summary>
        private Rigidbody2D m_Rigid;

        /// <summary>
        /// ������������ ���������� �������
        /// </summary>
        [SerializeField] private int m_MaxEnergy;

        /// <summary>
        /// ������������ ���������� ��������
        /// </summary>
        [SerializeField] private int m_MaxAmmo;

        /// <summary>
        /// ���������� ������� � �������
        /// </summary>
        [SerializeField] private float m_EnergyRegenPerSecond;

        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        private float m_PrimaryEnergy;

        /// <summary>
        /// ������� ���������� ��������
        /// </summary>
        private int m_SecondaryAmmo;

        #region Public API

        /// <summary>
        /// ���������� �������� �����. �� -1.0 �� 1.0.
        /// </summary>
        public float ThrustControl { get; set; }


        /// <summary>
        /// ���������� ������������ �����. �� -1.0 �� 1.0.
        /// </summary>
        public float TorqueControl { get; set; }

        #endregion

        #region Unity Event

        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;
            m_Rigid.inertia = 1.0f;

            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        private void Update()
        {

        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
            UpdateEnergyRegen();
        }

        #endregion

        /// <summary>
        /// ��������� ��� ������� ��� ��������
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigid.AddForce(m_Thrust * ThrustControl * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        public void Fire(TurretMode mode)
        {
            foreach(Turret turret in m_Turrets)
            {
                if (turret.Mode == mode)
                {
                    turret.Fire();
                }
            }
        }

        public void AddEnergy(int energy)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);
        }

        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }

        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy,0, m_MaxEnergy);
        }

        public bool DrawAmmo(int count)
        {

            if (m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }

            return false;
        }
        public bool DrawEnergy(int count)
        {

            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }

            return false;
        }

        public void AssignWeapon(TurretProperties properties)
        {
            foreach(Turret turret in m_Turrets)
            {
                turret.AssignLoadout(properties);
            }
        }
    }
}