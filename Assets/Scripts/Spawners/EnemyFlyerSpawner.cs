using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyerSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    // public Dictionary<EnemyGlobalScript, int> enemy_types;

    // Start is called before the first frame update
    void Start()
    {


        for (int i = 0; i < spawnPoints.Length; i++)
        {
            SpawnEnemys(spawnPoints[i].position);
        }
    }

    void SpawnEnemys(Vector3 pos)
    {
        EnemyGlobalScript enemy = EnemyFlyersFactory.instance.GetObjFromPool();
        enemy.transform.position = pos;
    }
}
