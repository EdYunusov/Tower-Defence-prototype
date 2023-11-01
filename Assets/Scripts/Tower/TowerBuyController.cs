using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuyController : MonoBehaviour
{

    [SerializeField] private TowerAsset m_TowerAsset;
    public void SetTowerAsset(TowerAsset asset) { m_TowerAsset = asset; }
    [SerializeField] private Text text;
    [SerializeField] private Button button;
    [SerializeField] private Transform buildSite;
    [SerializeField] public Tower towerPrefab;

    public Transform BuildSite { set { buildSite = value; } }


    private void Start()
    {
        TDPlayer.Instance.GoldUpdateSub(GoldStatCheck);
        text.text = m_TowerAsset.goldCost.ToString();
        button.GetComponent<Image>().sprite = m_TowerAsset.towerIcone;
    }

    private void GoldStatCheck(int gold)
    {
        if (gold >= m_TowerAsset.goldCost != button.interactable)
        {
            button.interactable = !button.interactable;
            text.color = button.interactable ? Color.white : Color.gray;
        }
    }

    public void Buy()
    {
        TDPlayer.Instance.TryBuild(m_TowerAsset, buildSite);
        TowerBuildSite.HideControls();
    }

    public void SetBuildSite(Transform value)
    {
        buildSite = value;
    }
}
