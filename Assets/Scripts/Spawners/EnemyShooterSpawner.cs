using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    [SerializeField] Transform _root;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < EnemyShooterFactory.instance.initialAmount; i++)
        {
            SpawnEnemys(spawnPoints[i].position);
        }
    }

    void SpawnEnemys(Vector3 pos)
    {
        EnemyGlobalScript enemy = EnemyShooterFactory.instance.GetObjFromPool();
        enemy.transform.position = pos;
        enemy.transform.parent = _root;
    }
}
