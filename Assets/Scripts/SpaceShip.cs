using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// Масса для автоматической установки у ригида.
        /// </summary>
        [Header("SpaceShip")]
        [SerializeField] private float mass;

        /// <summary>
        /// Толкающая вперед сила.
        /// </summary>
        [SerializeField] private float thrust;

        /// <summary>
        /// Вращающая сила.
        /// </summary>
        [SerializeField] private float mobility;

        /// <summary>
        /// Максимальная линейная скорость.
        /// </summary>
        [SerializeField] private float maxLinearVelocity;

        /// <summary>
        /// Максимальная вращательная скорость в градусах/сек.
        /// </summary>
        [SerializeField] private float maxAngularVelocity;

        /// <summary>
        /// Сохраненная ссылка на ригид
        /// </summary>
        private Rigidbody2D rigid;

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

            rigid = GetComponent<Rigidbody2D>();
            rigid.mass = mass;
            rigid.inertia = 1.0f;
        }

        private void Update()
        {

        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
        }

        #endregion

        /// <summary>
        /// Добавляет сил короблю для движения
        /// </summary>
        private void UpdateRigidBody()
        {
            rigid.AddForce(thrust * ThrustControl * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);
        
            rigid.AddForce(-rigid.velocity * (thrust / maxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            rigid.AddTorque(TorqueControl * mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            rigid.AddTorque(-rigid.angularVelocity * (mobility / maxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }
    }
}