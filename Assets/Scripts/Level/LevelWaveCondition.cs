using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaseShooter;

public class LevelWaveCondition : MonoBehaviour, ILevelCondition
{
    private bool isComplited;

    private void Start()
    {
        FindObjectOfType<WaveManager>().OnAllWavesDead += () =>
        {
            isComplited = true;
        };
    }
    public bool IsComplited { get { return isComplited; } }
}
