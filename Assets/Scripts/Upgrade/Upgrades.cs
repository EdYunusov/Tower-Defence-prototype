using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using SpaseShooter;

public class Upgrades : SingletonBase<Upgrades>
{
    public const string filename = "upgrade.dat";

    

    [Serializable] 
    private class UpgradeSave
    {
        public UpgradeAsset assest;
        public int level = 0;

    }

    private new void Awake()
    {
        base.Awake();
        Saver<UpgradeSave[]>.TryLoad(filename, ref save);
    }

    [SerializeField] private UpgradeSave[] save;
    public static void BuyUpgrade(UpgradeAsset asset)
    {
        foreach(var upgrade in Instance.save)
        {
            if(upgrade.assest == asset)
            {
                upgrade.level += 1;
                Saver<UpgradeSave[]>.Save(filename, Instance.save);
            }
        }
    }


    public static int GetTotalCost()
    {
        int result = 0;

        foreach(var upgrade in Instance.save)
        {
            for(int i = 0; i < upgrade.level; i++)
            {
                result += upgrade.assest.costByLevel[i];
            }    
        }
        return result;
    }

    public static int GetUpgradeLevel(UpgradeAsset asset)
    {
        foreach (var upgrade in Instance.save)
        {
            if (upgrade.assest == asset)
            {
                return upgrade.level;
            }
        }
        return 0;
    }
}
