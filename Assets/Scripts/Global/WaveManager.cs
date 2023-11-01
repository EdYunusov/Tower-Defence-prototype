using SpaseShooter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static Action<Enemy> OnEnemySpawn;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Path[] paths;
    [SerializeField] private EnemyWave currentWave;
    [SerializeField] private int activeEnemy = 0;

    public event Action OnAllWavesDead;

    private void RecordEnemyDead() 
    { 
        if(--activeEnemy == 0)
        {
            ForceNextWave();
        } 
    }
    

    private void Start()
    { 
        currentWave.Prepare(SpawnEnemies);
    }

    public void ForceNextWave()
    {
        if(currentWave)
        {
            TDPlayer.Instance.ChangeGold((int)currentWave.GetRemaningTime());
            SpawnEnemies();
        }
        else
        {
            if(activeEnemy ==0)
            {
                OnAllWavesDead?.Invoke();
            }
        }
    }

    private void SpawnEnemies()
    {
        foreach ((EnemyAssets asset, int count, int pathIndex) in currentWave.EnumerateSquads())
        {
            if(pathIndex < paths.Length)
            {
                for(int i = 0; i < count; i++)
                {
                    var e = Instantiate(enemyPrefab, paths[pathIndex].StartArea.GetRandomInsideZone(), Quaternion.identity);
                    e.Use(asset);
                    e.GetComponent<TDPatrolController>().SetPath(paths[pathIndex]);
                    activeEnemy += 1;
                    e.OnEnemyDeath += RecordEnemyDead;
                    OnEnemySpawn?.Invoke(e);
                }
            }
            else
            {
                Debug.LogWarning($"Invalid pathIndex in {name}");
            }
        }

        currentWave = currentWave.PrepareNext(SpawnEnemies);
    }
}
