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
        SecondZone.SecondZoneEvent += SpawnEnemys;
    }

    void SpawnEnemys()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            EnemyGlobalScript enemy = EnemyShooterFactory.instance.GetObjFromPool();
            enemy.transform.position = spawnPoints[i].position;
            enemy.transform.parent = _root;
            
        }

        AudioManager.instance.Play(AudioManager.Sounds.SpawnEnemies);
    }
}
