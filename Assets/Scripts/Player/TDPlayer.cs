using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaseShooter;
using System;
using UnityEngine.Events;

public class TDPlayer : Player
{
    public static new TDPlayer Instance
    {
        get
        {
            return Player.Instance as TDPlayer;
        }
    }

    [SerializeField] private int m_gold = 0;
    public int Gold => m_gold;

    [SerializeField] private int m_mana = 0;
    public int Mana => m_mana;

    private event Action<int> OnGoldUpdate;
    public void GoldUpdateSub(Action<int> act)
    {
        OnGoldUpdate += act;
        act(Instance.m_gold);
    }

    public event Action<int> OnLifeUpdate;

    public void LifeUpdateSub(Action<int> act)
    {
        OnLifeUpdate += act;
        act(Instance.HitPoints);
    }

    private event Action<int> OnManaUpdate;
    public void ManaUpdateSub(Action<int> act)
    {
        OnManaUpdate += act;
        act(Instance.m_mana);
    }

    public void ChangeGold(int change)
    {
        m_gold += change;
        OnGoldUpdate(m_gold);
    }

    [SerializeField] protected UnityEvent m_TakeDamage;

    public UnityEvent TakeDamageFromEnemy => m_TakeDamage;

    public void ChangeLife(int change)
    {
        TakeDamage(change);
        OnLifeUpdate(HitPoints);
    }

    public void ChangeMana(int change)
    {
        m_mana += change;
        OnManaUpdate(m_mana);
    }


    public void TryBuild(TowerAsset towerAssets, Transform buildSite)
    {
        ChangeGold(-towerAssets.goldCost);
        var buildtower = Instantiate(towerAssets.TowerPrefab, buildSite.position, Quaternion.identity);
        Destroy(buildSite);
    }

    [SerializeField] private UpgradeAsset hpUpgrade;

    private void Start()
    {
        var level = Upgrades.GetUpgradeLevel(hpUpgrade);
        TakeDamage(-level * 5);
    }
}
