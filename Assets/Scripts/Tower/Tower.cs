using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaseShooter;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Tower : MonoBehaviour
{
#if UNITY_EDITOR
    private static Color GizmoColor = new Color(0, 1, 0, 0.3f);
#endif

    [SerializeField] private UpgradeAsset fireRateUpgrade;
    //[SerializeField] private UpgradeAsset radiusUpgrade;
    [SerializeField] private float m_Radius;
    [SerializeField] private Turret[] turrets;
    [SerializeField] private float m_Lead = 0.1f;


    private Rigidbody2D target = null;

    public float Radius => m_Radius;

    public void Use(TowerAsset asset)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = asset.towerVisualModel;
        turrets = GetComponentsInChildren<Turret>();
        foreach (var turret in turrets)
        {
            turret.AssignLoadout(asset.turretProperties);
        }
        GetComponentInChildren<TowerBuildSite>().SetBuildableTowers(asset.m_UpgradesTo);
    }

    private void Start()
    {
        turrets = GetComponentsInChildren<Turret>();
    }

    private void Update()
    {
        if(target)
        {
            
            if (Vector3.Distance(target.transform.position, transform.position) <= m_Radius)
            {
                foreach (var turret in turrets)
                {
                    turret.transform.up = target.transform.position - turret.transform.position + (Vector3)target.velocity * m_Lead;
                    turret.Fire();

                    var projectile = turret.Fire();
                    
                    if(fireRateUpgrade != null && projectile !=null)
                    {
                        projectile.FireSpeedUpgrade(Upgrades.GetUpgradeLevel(fireRateUpgrade));
                    }
                }
            }
            else
            {
                target = null;
            }
        }
        else
        {
            var enter = Physics2D.OverlapCircle(transform.position, m_Radius);
            if (enter)
            {
                target = enter.transform.root.GetComponent<Rigidbody2D>();
            }
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawWireSphere(transform.position, m_Radius);
    }
#endif
}
