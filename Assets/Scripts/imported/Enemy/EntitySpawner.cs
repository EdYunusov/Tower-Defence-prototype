using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaseShooter
{
    public class EntitySpawner : Spawner
    {
        [SerializeField] private GameObject[] m_EntityPrefabs;
        protected override GameObject GenerateSpawnEntitys()
        {
            return Instantiate(m_EntityPrefabs[Random.Range(0, m_EntityPrefabs.Length)]);
        }    
    }


}
