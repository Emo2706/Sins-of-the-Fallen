using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyerSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    [SerializeField] Transform _root;

    // Start is called before the first frame update
    void Start()
    {


        for (int i = 0; i < EnemyFlyersFactory.instance.initialAmount; i++)
        {
            SpawnEnemys(spawnPoints[i].position);
        }
    }

    void SpawnEnemys(Vector3 pos)
    {
        EnemyGlobalScript enemy = EnemyFlyersFactory.instance.GetObjFromPool();
        enemy.transform.position = pos;
        enemy.transform.parent = _root;
    }
}
