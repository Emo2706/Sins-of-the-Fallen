using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalSpawner : MonoBehaviour
{
    public Transform[] spawnPointsFirstZone;
    public Transform[] spawnPointsSecondZone;
    [SerializeField] Transform _root;


    // Start is called before the first frame update
    void Start()
    {
        ColliderFirstZone.FirstZone += SpawnEnemysFirstZone;
        SecondZone.SecondZoneEvent += SpawnEnemysSecondZone;
      
    }

    void SpawnEnemysFirstZone()
    {
        for (int i = 0; i < spawnPointsFirstZone.Length; i++)
        {
            EnemyGlobalScript enemy = EnemyFactory.instance.GetObjFromPool();
            enemy.transform.position = spawnPointsFirstZone[i].position;
            enemy.transform.parent = _root;
        }

        AudioManager.instance.Play(AudioManager.Sounds.SpawnEnemies);
    }


    void SpawnEnemysSecondZone()
    {
        for (int i = 0; i < spawnPointsSecondZone.Length; i++)
        {
            EnemyGlobalScript enemy = EnemyFactory.instance.GetObjFromPool();
            enemy.transform.position = spawnPointsSecondZone[i].position;
            enemy.transform.parent = _root;
        }

        AudioManager.instance.Play(AudioManager.Sounds.SpawnEnemies);
    }
}
