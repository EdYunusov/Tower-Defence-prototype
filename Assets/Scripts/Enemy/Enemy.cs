using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaseShooter;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;


[RequireComponent(typeof(Destructible))]
[RequireComponent(typeof(TDPatrolController))]
public class Enemy : MonoBehaviour
{

    public enum TypeOfArmor { Base = 0, Mage = 1 }

    private static Func<int, TDProjectile.TypeOfDagame, int, int>[] ArmorDamageFunctions =
    { // Base Armor

        (int power, TDProjectile.TypeOfDagame type, int armor ) =>
        {
            switch (type)
            {
                case TDProjectile.TypeOfDagame.Magic: return power;
                default: return Mathf.Max(power - armor, 1);
            }
        },

         (int power, TDProjectile.TypeOfDagame type, int armor ) =>
         {// Mage Armor

             if( TDProjectile.TypeOfDagame.Base == type) armor = armor / 2;
             return Mathf.Max(power - armor, 1);
         }

    };

    [SerializeField] private int damage = 1;
    [SerializeField] private int armor = 1;
    [SerializeField] private int gold = 1;
    [SerializeField] private TypeOfArmor armorType;

    private Destructible destructible;

    private void Awake()
    {
        destructible = GetComponent<Destructible>();
    }

    public event Action OnEnemyDeath;

    private void OnDestroy()
    {
        OnEnemyDeath();
    }

    public void Use(EnemyAssets asset)
    {

        var sr = transform.Find("VisualModel").GetComponent<SpriteRenderer>();
        var coll = transform.Find("Collider").GetComponent<CircleCollider2D>();

        coll.radius = asset.Radius;

        sr.transform.localScale = new Vector3(asset.spriteScale.x, asset.spriteScale.y, 1);
        sr.color = asset.color;

        //sr.GetComponent<Animator>().runtimeAnimatorController = asset.animatios;

        GetComponent<SpaceShip>().Use(asset);

        damage = asset.damage;
        armorType = asset.armorType;
        armor = asset.armor;
        gold = asset.gold;
    }

    public void TakeDamage(int damage, TDProjectile.TypeOfDagame damageType)
    {
        destructible.ApplayDamage(ArmorDamageFunctions[(int)armorType](damage, damageType, armor));
    }

    public void DamageOnEndPath()
    {
        TDPlayer.Instance.ChangeLife(damage);
    }

    public void GiveGold()
    {
        TDPlayer.Instance.ChangeGold(gold);
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]

    public class EnemyInspector: Editor
    {
        public override void OnInspectorGUI()
        {
            EnemyAssets a = EditorGUILayout.ObjectField(null, typeof(EnemyAssets), false) as EnemyAssets;

            if (a)
            {
                (target as Enemy).Use(a);
            }
        }
    }
#endif

}
