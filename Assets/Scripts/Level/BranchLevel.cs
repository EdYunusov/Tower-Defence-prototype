using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(MapLevel))]
public class BranchLevel : MonoBehaviour
{
    [SerializeField] private Text pointText;
    [SerializeField] private MapLevel rootLevel;
    [SerializeField] private int needPoints;

    internal void TryActive()
    {
        gameObject.SetActive(rootLevel.IsComplited);
        
        if(needPoints > MapCompletion.Instance.TotalScore)
        {
            
            pointText.text = needPoints.ToString();

        }
        else
        {
            print($"unlock with");
            pointText.transform.parent.gameObject.SetActive(false);
            GetComponent<MapLevel>().Initialize();
        }
        
    }
}
