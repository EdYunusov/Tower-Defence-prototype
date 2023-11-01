using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaseShooter
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_HP;
        private SpaceShip m_Ship;
        private GameObject m_PlayerShipPrefab;

        public int HitPoints
        {
            get
            {
                return m_HP;
            }
        }

        public SpaceShip ActiveShip => m_Ship;

        protected override void Awake()
        {
            base.Awake();

            if (m_Ship != null) Destroy(m_Ship.gameObject);

        }

        private void Start()
        {
            if(m_Ship)
            {
                Respawn();
            }

        }

        private void OnShopDeath()
        {
            m_HP--;

            if (m_HP > 0)
            {
                Respawn();
            }
            else
            {
                LevelSequenceController.Instance.FinishCurrentLevel(false);
            }
        }

        internal void GetGold(int gold)
        {
            throw new NotImplementedException();
        }

        public event Action OnPlayerDead;

        protected void TakeDamage(int damage)
        {
            m_HP -= damage;
            
            if(m_HP <= 0)
            {
                m_HP = 0;
                OnPlayerDead?.Invoke();
            }
        }

        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

                m_Ship = newPlayerShip.GetComponent<SpaceShip>();

                //m_CameraController.SetTarget(m_Ship.transform);
                //m_MovementController.SetTargetShip(m_Ship);

                m_Ship.EventOnDeath.AddListener(OnShopDeath);
            }
        }

        #region Score

        public int killCount { get; private set; }

        public int NumKill { get; private set; }

        public void AddKil()
        {
            NumKill++;
        }

        public void AddKillCount(int num)
        {
            killCount += num;
        }
        #endregion
    }
}

