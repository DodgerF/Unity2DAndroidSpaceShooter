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

        [SerializeField] private AIPointPatrol m_PatrolPoint;

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
            FindNewAttackTarget();
            Fire();
        }

        private void Fire()
        {

        }

        private void FindNewAttackTarget()
        {
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null)
                {
                    m_MovePosition = m_SelectedTarget.transform.position;
                }
                else
                {
                    if (m_PatrolPoint != null)
                    {
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;

                        if (isInsidePatrolZone)
                        {
                            if (m_RandomizeDirectionTimer.IsFinnished)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius + m_PatrolPoint.transform.position;

                                m_MovePosition = newPoint;

                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                            }
                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }
                }
            }
        }

        private void ControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLinear;
            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        private void FindNewPosition()
        {
            
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

        public void SerPatrolBehaviour(AIPointPatrol pointPatrol)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoint = pointPatrol;
        }

        #region Timers

        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
        }

        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
        }

        #endregion
    }
}