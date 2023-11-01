using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaseShooter;

public class TDProjectile : Projectile
{
    public enum TypeOfDagame { Base, Magic}
    [SerializeField] private TypeOfDagame type;
    [SerializeField] private Sound m_ShootSound = Sound.ArrowShoot;
    [SerializeField] private Sound m_HitSound = Sound.ArrowHit;
    private Destructible m_Parent;


    private void Start()
    {
        m_ShootSound.Play();
    }


    protected override void OnHit(RaycastHit2D hit)
    {
        m_HitSound.Play();

        var enemy = hit.collider.transform.root.GetComponent<Enemy>();
        if (enemy != null && enemy != m_Parent)
        {
            enemy.TakeDamage(m_Damage, type);
        }
        
    }
}
