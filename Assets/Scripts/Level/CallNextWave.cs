using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallNextWave : MonoBehaviour
{
    [SerializeField] private Text bonusAmount;
    private WaveManager waveManager;
    private float timeToNextWave;


    private void Start()
    {

        EnemyWave.OnWavePrepare += (float time) =>
        {
            timeToNextWave = time;
        };
        waveManager = FindObjectOfType<WaveManager>();
    }


    public void CallWave()
    {
        waveManager.ForceNextWave();
    }

    private void Update()
    {
        var bonus = (int)timeToNextWave;
        if(bonus < 0)
        {
            bonus = 0;
        }
        bonusAmount.text = bonus.ToString();
        timeToNextWave -= Time.deltaTime;
    }


}
