using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUpgradeShop : MonoBehaviour
{
    [SerializeField] private GameObject storePanel;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void StoreButton()
    {
        gameObject.SetActive(true);
    }

    public void StoreBack()
    {
        gameObject.SetActive(false);
    }
}
