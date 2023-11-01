using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaseShooter;

public class TDLevelController : LevelController
{
    private int levelScore = 3;
    private new void Start()
    {
        base.Start();
        TDPlayer.Instance.OnPlayerDead += () =>
        {
            StopLevelActivity();
            ResultPanelController.Instance.Show(false);
        };

        m_ReferenceTime += Time.time;

        m_EventLevelConplited.AddListener(() =>
        {
            StopLevelActivity();

            if(m_ReferenceTime <= Time.time)
            {
                levelScore -= 1;
            }
            print(levelScore);
            MapCompletion.SaveEpisodeResult(levelScore);
        });

        void LifeScoreChange(int _)
        {
            levelScore -= 1;
            TDPlayer.Instance.OnLifeUpdate -= LifeScoreChange;
        }

        TDPlayer.Instance.OnLifeUpdate += LifeScoreChange; 

    }

    private void StopLevelActivity()
    {
        Debug.Log("level end");

        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            enemy.GetComponent<SpaceShip>().enabled = false;
            enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        
        void DisableAll<T>() where T: MonoBehaviour
        {
            foreach(var obj in FindObjectsOfType<T>())
            {
                obj.enabled = false;
            }
        }

        DisableAll<Spawner>();
        DisableAll<Projectile>();
        DisableAll<Tower>();
        DisableAll<CallNextWave>();
    }
}
