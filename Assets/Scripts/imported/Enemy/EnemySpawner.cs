using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaseShooter
{
    public class EnemySpawner : Spawner
    {
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private Path m_path;
        [SerializeField] private EnemyAssets[] enemySettings;


        protected override GameObject GenerateSpawnEntitys()
        {

            var e = Instantiate(enemyPrefab);
            e.Use(enemySettings[Random.Range(0, enemySettings.Length)]);

            e.GetComponent<TDPatrolController>().SetPath(m_path);

            return e.gameObject;
        }
    }


}
