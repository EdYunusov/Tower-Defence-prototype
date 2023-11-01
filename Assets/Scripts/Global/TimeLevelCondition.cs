using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaseShooter;

public class TimeLevelCondition : MonoBehaviour, ILevelCondition
{
    [SerializeField] private float timeLimited;
    private void Start()
    {
        timeLimited *= Time.deltaTime;
    }
    public bool IsComplited => Time.time > timeLimited;
}
