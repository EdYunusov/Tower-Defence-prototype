using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaseShooter
{
    public class Destructible : Entity
    {
        #region Properties
        [SerializeField] protected bool m_Indestructible;
        [SerializeField] private int m_HitPoints;

        private int m_CurrentHitPoints;
        private Rigidbody2D m_Rig;

        public HealthBar m_healthBar;

        public bool IsIndestructible => m_Indestructible;
        public int HitPoints => m_CurrentHitPoints;
        #endregion

        

        #region Unity Events

        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
            m_Rig = GetComponent<Rigidbody2D>();

            if(m_healthBar != null) m_healthBar.SetMaxHealth(m_HitPoints);
        }
        public Vector3 ObjectVelocity => m_Rig.velocity;

        #endregion

        #region Public API

        public void ApplayDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_healthBar != null) m_healthBar.SetHealth(m_CurrentHitPoints);

            if (m_CurrentHitPoints < 0)
            {
                OnDeath();
            }
        }
        #endregion

        protected virtual void OnDeath()
        {
            Destroy(gameObject);
            m_EventOnDeath?.Invoke();
        }

        private static HashSet<Destructible> m_AllDestructibles;
        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;
        
        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null) m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        public const int TEAMIDNEUTRAL = 0;
        [SerializeField] private int m_TeamID;
        public int TeamId => m_TeamID;

        [SerializeField] protected UnityEvent m_EventOnDeath;

        public UnityEvent EventOnDeath => m_EventOnDeath;

        #region Score

        [SerializeField] private int m_ScoreValue;

        public int ScoreValue => m_ScoreValue;
        #endregion

        protected void Use(EnemyAssets assets)
        {
            m_HitPoints = assets.HP;
            m_ScoreValue = assets.Score;
        }
    }

}
