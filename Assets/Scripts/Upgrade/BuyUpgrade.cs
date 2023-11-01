using SpaseShooter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuyUpgrade : MonoBehaviour
{


    [SerializeField] private UpgradeAsset asset;
    //[SerializeField] private Image upgradeIcone;
    [SerializeField] private Text level, costText;
    [SerializeField] private Button buyButton;

    private int costNum;

    public void Initialize()
    {
        //upgradeIcone.sprite = asset.sprite;

        var savedlevel = Upgrades.GetUpgradeLevel(asset);

        

        if(savedlevel >= asset.costByLevel.Length)
        {
            level.text = $"lvl: {savedlevel} (Max)";
            buyButton.interactable = false;
            //buyButton.transform.Find("Image (1)").gameObject.SetActive(false);
            //buyButton.transform.Find("Text").gameObject.SetActive(false);

            costText.text = "X";
            costNum = int.MaxValue;
        }
        else
        {
            level.text = $"lvl: {savedlevel + 1}";
            costNum = asset.costByLevel[savedlevel];
            costText.text = costNum.ToString();
        }

    }

    internal void CheckCost(int money)
    {
        buyButton.interactable = money >= costNum;
    }

    public void Buy()
    {
        Upgrades.BuyUpgrade(asset);
    }
}
