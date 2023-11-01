using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpaseShooter;


public class Abilitis : SingletonBase<Abilitis>
{
    public interface IUsable { void Use(); }

    [Serializable]

    public class StrikeSkill : IUsable
    {
        [SerializeField] private int m_Cost = 20;
        [SerializeField] private int m_Damage = 10;
        [SerializeField] private Color m_TargetingColor;
        [SerializeField] private UpgradeAsset requiredUpgrade;
        [SerializeField] private Button m_UseFireButton;

        public void CheckAbility(int money)
        {
            m_UseFireButton.interactable = money >= m_Cost && CanUse();
        }

        public bool CanUse()
        {
            return Upgrades.GetUpgradeLevel(requiredUpgrade) > 0;
        }

        public void Use() 
        {
            if (!CanUse()) return;

            if (TDPlayer.Instance.Mana >= m_Cost)
            {
                TDPlayer.Instance.ChangeMana(-m_Cost);
            }

            ClickProtection.Instance.Activate((Vector2 v) =>
            {
                Vector3 position = v;
                position.z = -Camera.main.transform.position.z;
                position = Camera.main.ScreenToWorldPoint(position);

                foreach(var collider in Physics2D.OverlapCircleAll(position, 5))
                {
                    if(collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                    {
                        enemy.TakeDamage(m_Damage, TDProjectile.TypeOfDagame.Magic);
                    }
                }
            });
        }
    }


    [Serializable]
    public class SlowSkill : IUsable
    {
        [SerializeField] private int m_Cost = 10;
        [SerializeField] private float m_Cooldown = 15f;
        [SerializeField] private float m_Duration = 5f;
        [SerializeField] private UpgradeAsset requiredUpgrade;
        [SerializeField] private Button m_UseSlowButton;

        public void CheckAbility(int money)
        {
            m_UseSlowButton.interactable = money >= m_Cost && CanUse();
        }

        public bool CanUse()
        {
            return Upgrades.GetUpgradeLevel(requiredUpgrade) > 0;
        }

        public void Use()
        {
            if (!CanUse()) return;

            if (TDPlayer.Instance.Mana >= m_Cost)
            {
                TDPlayer.Instance.ChangeMana(-m_Cost);
            }

            void Slow(Enemy ship)
            {
                ship.GetComponent<SpaceShip>().HalfMaxLinearVelosity();
            }
            foreach (var ship in FindObjectsOfType<SpaceShip>()) ship.HalfMaxLinearVelosity();

            WaveManager.OnEnemySpawn += Slow;

            IEnumerator Restore()
            {
                yield return new WaitForSeconds(m_Duration);
                foreach (var ship in FindObjectsOfType<SpaceShip>()) ship.RecoverfMaxLinearVelosity();
                WaveManager.OnEnemySpawn -= Slow;
            }
            Instance.StartCoroutine(Restore());

            //IEnumerator TimeAbilityButton()
            //{
            //    Instance.m_TimeButton.interactable = false;
            //    yield return new WaitForSeconds(m_Cooldown);
            //    Instance.m_TimeButton.interactable = true;
            //}
        } 
    }

    [SerializeField] private Button m_TimeButton;
    [SerializeField] private Image m_TargetCircal;

    [SerializeField] private StrikeSkill m_StrikeSkill;
    public void UseStrikeSkill() => m_StrikeSkill.Use();

    [SerializeField] private SlowSkill m_SlowSkill;
    public void UseSlowSkill() => m_SlowSkill.Use();
}
