using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NowBuildSite : TowerBuildSite
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        HideControls();
    }
}
