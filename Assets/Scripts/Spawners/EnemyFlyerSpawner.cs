using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyerSpawner : MonoBehaviour
{
    public Transform[] spawnPointsLeftZone;
    public Transform spawnPointRightZone;
    [SerializeField] Transform _root;

    // Start is called before the first frame update
    void Start()
    {
        LeftZone.LeftZoneEvent += SpawnEnemysLeftZone;
        SecondZone.SecondZoneEvent += SpawnEnemyRightZone;
        
    }

    void SpawnEnemysLeftZone()
    {

        for (int i = 0; i < spawnPointsLeftZone.Length; i++)
        {
            EnemyFlyers enemy = EnemyFlyersFactory.instance.GetObjFromPool();
            enemy.transform.position = spawnPointsLeftZone[i].position;
            enemy.transform.parent = _root;
        }

        AudioManager.instance.Play(AudioManager.Sounds.SpawnEnemies);

    }

    void SpawnEnemyRightZone()
    {
        EnemyFlyers enemy = EnemyFlyersFactory.instance.GetObjFromPool();
        enemy.transform.position = spawnPointRightZone.position;
        enemy.transform.parent = _root;
        enemy.isInRightZone = true;
        AudioManager.instance.Play(AudioManager.Sounds.SpawnEnemies);
    }
}
