using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    // public Dictionary<EnemyGlobalScript, int> enemy_types;

    // Start is called before the first frame update
     void Start()
    {
       

        for (int i = 0; i < EnemyFactory.instance.initialAmount; i++)
        {
            SpawnEnemys(spawnPoints[i].position);
        }
    }

     void SpawnEnemys(Vector3 pos)
    {
        EnemyGlobalScript enemy = EnemyFactory.instance.GetObjFromPool();
        enemy.transform.position = pos;
    }
}
