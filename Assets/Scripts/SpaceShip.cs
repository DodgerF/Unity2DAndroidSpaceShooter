using Unity.VisualScripting;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// ����� ��� �������������� ��������� � ������.
        /// </summary>
        [Header("SpaceShip")]
        [SerializeField] private float mass;

        /// <summary>
        /// ��������� ������ ����.
        /// </summary>
        [SerializeField] private float thrust;

        /// <summary>
        /// ��������� ����.
        /// </summary>
        [SerializeField] private float mobility;

        /// <summary>
        /// ������������ �������� ��������.
        /// </summary>
        [SerializeField] private float maxLinearVelocity;

        /// <summary>
        /// ������������ ������������ �������� � ��������/���.
        /// </summary>
        [SerializeField] private float maxAngularVelocity;

        /// <summary>
        /// ������ ��������
        /// </summary>
        [SerializeField] private Turret[] m_Turrets;

        /// <summary>
        /// ����������� ������ �� �����
        /// </summary>
        private Rigidbody2D rigid;

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
        /// ��������� ��� ������� ��� ��������
        /// </summary>
        private void UpdateRigidBody()
        {
            rigid.AddForce(thrust * ThrustControl * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            rigid.AddForce(-rigid.velocity * (thrust / maxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            rigid.AddTorque(TorqueControl * mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            rigid.AddTorque(-rigid.angularVelocity * (mobility / maxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
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


    }
}