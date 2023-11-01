using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class TowerBuildSite : MonoBehaviour, IPointerDownHandler
{
    public TowerAsset[] buildableTowers;
    public void SetBuildableTowers(TowerAsset[] towers)
    {
        if (towers == null || towers.Length == 0)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            buildableTowers = towers;
        }
    }

    public static event Action<TowerBuildSite> OnClickEvent;


    public static void HideControls()
    {
        OnClickEvent(null);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnClickEvent(this);
    }
}
