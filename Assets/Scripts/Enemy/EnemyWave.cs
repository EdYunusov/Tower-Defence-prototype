using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using SpaseShooter;

public class EnemyWave: MonoBehaviour
{

    [Serializable]
    private class Squad
    {
        public EnemyAssets assets;
        public int count;
    }

    [Serializable]

    private class PathGroup
    {
        public Squad[] squads;
    }

    [SerializeField] private PathGroup[] groups;
    [SerializeField] private float prepareTime = 10f;

    public float GetRemaningTime() { return prepareTime - Time.time; }

    private void Awake()
    {
        enabled = false;

    }

    private event Action OnWaveReady;

    public void Prepare(Action spawnEnemies)
    {
        OnWavePrepare?.Invoke(prepareTime);
        prepareTime += Time.time;
        enabled = true;
        OnWaveReady += spawnEnemies;
    }

    private void Update()
    {
        if(Time.time >= prepareTime)
        {
            enabled = false;
            OnWaveReady?.Invoke();
        }
    }

    public IEnumerable<(EnemyAssets asset, int count, int pathIndex)> EnumerateSquads()
    {
        for(int i = 0; i < groups.Length; i++)
        {
            foreach(var squad in groups[i].squads)
            {
                yield return (squad.assets, squad.count, i);
            }
        }
    }

    [SerializeField] private EnemyWave next;

    public static Action<float> OnWavePrepare;

    public EnemyWave PrepareNext(Action spawnEnemies)
    {
        OnWaveReady -= spawnEnemies;
        if(next)
            next.Prepare(spawnEnemies);
        return next;
    }
}