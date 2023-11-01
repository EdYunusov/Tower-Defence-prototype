using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaseShooter
{
    public class Projectile : Entity
    {
        [SerializeField] private float m_Velocity;
        [SerializeField] private float m_LifeTime;
        [SerializeField] protected int m_Damage;
        [SerializeField] private ImpactEffect m_ImpactEffectPref;

        public void SetFromOtherProjectile(Projectile other)
        {
            other.GetData(out m_Velocity, out m_LifeTime, out m_Damage, out m_ImpactEffectPref);
        }

        private void GetData(out float m_Velocity, out float m_LifeTime, out int m_Damage, out ImpactEffect m_ImpactEffectPref)
        {
            m_Velocity = this.m_Velocity;
            m_LifeTime = this.m_LifeTime;
            m_Damage = this.m_Damage;
            m_ImpactEffectPref = this.m_ImpactEffectPref;
        }

        private float m_Timer;

        private void Update()
        {
            float stepLen = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLen;
            m_Timer += Time.deltaTime;
            if (m_Timer > m_LifeTime) Destroy(gameObject);
            
            transform.position += new Vector3(step.x, step.y, 0);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLen);
            if(hit)
            {
                OnHit(hit);
                OnProjectileLifeTimeEnd(hit.collider, hit.point);
            }
        }

        protected virtual void OnHit(RaycastHit2D hit)
        {
            var dist = hit.collider.transform.root.GetComponent<Destructible>();
            if (dist != null && dist != m_Parent)
            {
                dist.ApplayDamage(m_Damage);
            }
        }

        private void OnProjectileLifeTimeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }

        private Destructible m_Parent;

        public void SetParentShoot(Destructible parent)
        {
            m_Parent = parent;
        }

        //UpgradeSegmet
        public void FireSpeedUpgrade(float fireSpeed)
        {
            m_Velocity += fireSpeed;
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(Projectile))]
        public class ProjetileInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                if(GUILayout.Button("Create TD Projectile"))
                {
                    var target = this.target as Projectile;
                    var tdProj = target.gameObject.AddComponent<TDProjectile>();

                    tdProj.SetFromOtherProjectile(target);
                    
                }
            }
        }
#endif
    }
}

