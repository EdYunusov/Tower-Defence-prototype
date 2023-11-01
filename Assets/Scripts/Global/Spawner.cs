using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaseShooter;

public abstract class Spawner : MonoBehaviour
{
    protected abstract GameObject GenerateSpawnEntitys();


    [SerializeField] private SpawnMode m_SpawnMode;
    [SerializeField] private CircalArea m_Area;
    [SerializeField] private int m_NumSpawns;
    [SerializeField] private float m_RespTime;
    

    public enum SpawnMode
    {
        Start,
        Loop
    }

    private float m_Timer;

    private void Start()
    {
        if (m_SpawnMode == SpawnMode.Start)
        {
            SpawnEntities();
        }
        m_Timer = m_RespTime;
    }


    private void Update()
    {
        if (m_Timer > 0) m_Timer -= Time.deltaTime;

        if (m_SpawnMode == SpawnMode.Loop && m_Timer < 0)
        {
            SpawnEntities();
            m_Timer = m_RespTime;
        }
    }

    private void SpawnEntities()
    {
        for (int i = 0; i < m_NumSpawns; i++)
        {
            var e = GenerateSpawnEntitys();
            e.transform.position = m_Area.GetRandomInsideZone();
        }
    }
}
