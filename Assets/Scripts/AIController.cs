using System;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol
        }

        [SerializeField] public AIBehaviour m_AIBehaviour;

        [SerializeField] private AIZonePatrol m_PatolZone;

        [Range(0f, 1f)]
        [SerializeField] private float m_NavigationLinear;

        [Range(0f, 1f)]
        [SerializeField] private float m_NavigationAngular;

        [SerializeField] private float m_RandomSelectMovePointTime;
        [SerializeField] private float m_FindNewTargetTime;
        [SerializeField] private float m_ShootDelay;
        [SerializeField] private float m_EvadeRayLength;

        private SpaceShip m_SpaceShip;
        private Vector3 m_MovePosition;
        private Destructible m_SelectedTarget;

        private Timer m_RandomizeDirectionTimer;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;

        #region Unity Events
        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();

            InitTimers();
        }
        private void Update()
        {
            UpdateTimers();

            UpdateAI();
        }
        #endregion

        #region Logic
        private void UpdateAI()
        {
            if (m_AIBehaviour == AIBehaviour.Null) return;

            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateVehaviourPatrol();
            }
        }

        private void UpdateVehaviourPatrol()
        {
            FindNewPosition();
            ControlShip();
            EvadeCollision();
            FindNewAttackTarget();
            Fire();
        }

        private void Fire()
        {
            if (m_SelectedTarget != null)
            {
                if (m_FireTimer.IsFinnished)
                {
                    m_SpaceShip.Fire(TurretMode.Primary);
                    m_FireTimer.Start(m_ShootDelay);
                }
            }
        }

        private void FindNewAttackTarget()
        {
            if (m_FindNewTargetTimer.IsFinnished)
            {
                m_SelectedTarget = FindNearsDestructibleTarget();
                m_FindNewTargetTimer.Start(m_ShootDelay);
            }    
        }

        private Destructible FindNearsDestructibleTarget()
        {
            float minDist = float.MaxValue;
            Destructible target = null;

            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == this) continue;
                if (v.TeamId == Destructible.TeamIdNeutral) continue;
                if (v.TeamId == m_SpaceShip.TeamId) continue;

                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);          
                if (dist < minDist)
                {
                    minDist = dist;
                    target = v;
                }
            }

            return target;
        }

        private void EvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength))
            {
                m_MovePosition = transform.position + transform.right * 100.0f;
            }
        }
        
        private void ControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLinear;
            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        private void FindNewPosition()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null)
                {
                    m_MovePosition = MakeLead(
                        m_SpaceShip.transform.position, m_SpaceShip.transform.up * m_SpaceShip.FirstTurret.Property.ProjectilePrefab.Velocity,
                        m_SelectedTarget.transform.position, m_SelectedTarget.GetComponent<Rigidbody2D>().velocity);
                }
                else
                {
                    if (m_PatolZone != null)
                    {
                        bool isInsidePatrolZone = (m_PatolZone.transform.position - transform.position).sqrMagnitude < m_PatolZone.Radius * m_PatolZone.Radius;

                        if (isInsidePatrolZone)
                        {
                            if (m_RandomizeDirectionTimer.IsFinnished)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatolZone.Radius + m_PatolZone.transform.position;

                                m_MovePosition = newPoint;

                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            m_MovePosition = m_PatolZone.transform.position;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Метод вычисления точки упреждения.
        /// </summary>
        public static Vector3 MakeLead(
        Vector3 launchPoint,
        Vector3 launchVelocity,
        Vector3 targetPos,
        Vector3 targetVelocity)
        {
            Vector3 V = targetVelocity;
            Vector3 D = targetPos - launchPoint;
            float A = V.sqrMagnitude - launchVelocity.sqrMagnitude;
            float B = 2 * Vector3.Dot(D, V);
            float C = D.sqrMagnitude;

            if (A >= 0)
                return targetPos;

            float rt = Mathf.Sqrt(B * B - 4 * A * C);
            float dt1 = (-B + rt) / (2 * A);
            float dt2 = (-B - rt) / (2 * A);
            float dt = (dt1 < 0 ? dt2 : dt1);
            return targetPos + V * dt;
        }


        #endregion

        private const float MAX_ANGLE = 45f;
        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            //перевод координат в локальные
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);
            //угл поворота к таргету
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);
            //ограничение для максимального поворота 
            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            return -angle;
        }

        public void SerPatrolBehaviour(AIZonePatrol pointPatrol)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatolZone = pointPatrol;
        }

        #region Timers

        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
    }

        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
        }

        #endregion
    }
}