using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// Масса для автоматической установки у ригида.
        /// </summary>
        [Header("SpaceShip")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Толкающая вперед сила.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Вращающая сила.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Максимальная линейная скорость.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;

        /// <summary>
        /// Максимальная вращательная скорость в градусах/сек.
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;

        /// <summary>
        /// Массив туррелей
        /// </summary>
        [SerializeField] private Turret[] m_Turrets;

        /// <summary>
        /// Сохраненная ссылка на ригид
        /// </summary>
        private Rigidbody2D m_Rigid;

        /// <summary>
        /// Максимальный показатель энергии
        /// </summary>
        [SerializeField] private int m_MaxEnergy;

        /// <summary>
        /// Максимальный показатель патронов
        /// </summary>
        [SerializeField] private int m_MaxAmmo;

        /// <summary>
        /// Количество энергии в секунду
        /// </summary>
        [SerializeField] private float m_EnergyRegenPerSecond;

        /// <summary>
        /// Текущее количество энергии
        /// </summary>
        private float m_PrimaryEnergy;

        /// <summary>
        /// Текущее количество патронов
        /// </summary>
        private int m_SecondaryAmmo;

        #region Public API

        /// <summary>
        /// Управление линейной тягой. От -1.0 до 1.0.
        /// </summary>
        public float ThrustControl { get; set; }


        /// <summary>
        /// Управление вращательной тягой. От -1.0 до 1.0.
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
        /// Добавляет сил короблю для движения
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