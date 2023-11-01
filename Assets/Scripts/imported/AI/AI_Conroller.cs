using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaseShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AI_Conroller : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null,
            Patrol
        }

        [SerializeField] private AIBehaviour m_AIBehaivour;
        [SerializeField] private AI_PatrolZone m_PatrolZone;

        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;
        [SerializeField] private float m_RandomSelectMovePointTime;
        [SerializeField] private float m_FindNewTargetTime;
        [SerializeField] private float m_ShootDelay;
        [SerializeField] private float m_EvadeRayLength;
        [SerializeField] private float m_PatrolPointRange;
        [SerializeField] private float m_PredictionCoff;
        [SerializeField] private Transform[] m_PatrolPoints;
        


        private SpaceShip m_SpaceShip;
        private Vector3 m_MovePosition;
        private Destructible m_SelectedTarget;
        private Timer testTimer;
        private Timer randomizeDirectionTimer;
        private Timer fireTimer;
        private Timer findNewTargetTimer;
        private int m_PatrolPointCount = 0;



        private void Start()
        {
            m_SpaceShip = GetComponent<SpaceShip>();
            IntTimers();

            if(m_PatrolPoints.Length > 0)
            {
                m_PatrolPointCount = 0;
                m_MovePosition = m_PatrolPoints[0].position;
            }
        }

        private void Update()
        {
            UpdateTimer();
            UpdateAI();
        }

        private void UpdateAI()
        {
            if(m_AIBehaivour == AIBehaviour.Null)
            {

            }

            if(m_AIBehaivour == AIBehaviour.Patrol)
            {
                UpdateBeahaivourPatrol();
            }
        }

        public void UpdateBeahaivourPatrol()
        {
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionEvadeCollision();
        }
        public void SetPatrolBehavior_2(AI_PatrolZone zone)
        {
            m_AIBehaivour = AIBehaviour.Patrol;
            m_PatrolZone = zone;
        }

        private void ActionFindNewMovePosition()
        {
            if (m_AIBehaivour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null)
                {
                    m_MovePosition = m_SelectedTarget.transform.position + (m_SelectedTarget.ObjectVelocity * m_PredictionCoff);
                }
                else
                {
                    if(m_PatrolZone != null)
                    {
                        bool isInsidePatrolZone = (m_PatrolZone.transform.position - transform.position).sqrMagnitude < m_PatrolZone.Radius * m_PatrolZone.Radius;
                        
                        GetNewPoint(isInsidePatrolZone);
                    }
                    else if(m_PatrolPoints.Length > 0)
                    {
                        if((transform.position - m_MovePosition).magnitude <= m_PatrolPointRange)
                        {
                            m_PatrolPointCount++;

                            if(m_PatrolPointCount >= m_PatrolPoints.Length)
                            {
                                m_PatrolPointCount = 0;
                            }

                            m_MovePosition = m_PatrolPoints[m_PatrolPointCount].position;
                        }
                    }
                }
            }
        }

        protected virtual void GetNewPoint(bool isInsidePatrolZone)
        {
            if (isInsidePatrolZone)
            {
                if (randomizeDirectionTimer.IsFinished)
                {
                    m_MovePosition = (Vector3)m_PatrolZone.GetRandomInsideZone();

                    randomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                }
            }
            //else
            //{
            //    m_MovePosition = m_PatrolZone.transform.position;
            //}
        }

        private void ActionEvadeCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLength) == true)
            {
                m_MovePosition = transform.position + transform.right * 100.0f;
            }
        }

        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLinear;
            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        private const float MAX_ANGLE = 45.0f;
        private static float ComputeAliginTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPos = ship.InverseTransformPoint(targetPosition);
            float angle = Vector3.SignedAngle(localTargetPos, Vector3.up, Vector3.forward);
            angle = Mathf.Clamp(angle, -MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;
            return -angle;
        }

        private Destructible FindeNearestDestructableTarget()
        {
            float maxDist = float.MaxValue;
            Destructible potentialTarget = null;

            foreach (var v in Destructible.AllDestructibles)
            {
                if (v.GetComponent<SpaceShip>() == m_SpaceShip) continue;
                if (v.TeamId == Destructible.TEAMIDNEUTRAL) continue;
                if (v.TeamId == m_SpaceShip.TeamId) continue;

                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);

                if(dist < maxDist)
                {
                    maxDist = dist;

                    potentialTarget = v;
                }
            }

            return potentialTarget;
        }

        private void ActionFindNewAttackTarget()
        {
            if (findNewTargetTimer.IsFinished == true)
            {
                m_SelectedTarget = FindeNearestDestructableTarget();
                findNewTargetTimer.Start(m_ShootDelay);
            }
        }

        private void ActionFire()
        {
            if (m_SelectedTarget != null)
            {
                if (fireTimer.IsFinished == true)
                {
                    m_SpaceShip.Fire(TurretMode.Primary);
                    fireTimer.Start(m_ShootDelay);
                }
            }
        }

        #region Timers
        private void IntTimers()
        {
            randomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            fireTimer = new Timer(m_ShootDelay);
            findNewTargetTimer = new Timer(m_FindNewTargetTime);
        }

        private void UpdateTimer()
        {
            randomizeDirectionTimer.RemoveTime(Time.deltaTime);
            fireTimer.RemoveTime(Time.deltaTime);
            findNewTargetTimer.RemoveTime(Time.deltaTime);
        }
        #endregion

    }
}

