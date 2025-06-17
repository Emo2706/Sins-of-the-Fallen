using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyerSpawner : MonoBehaviour
{
    public Transform[] spawnPointsLeftZone;
    [SerializeField] Transform _root;

    // Start is called before the first frame update
    void Start()
    {
        LeftZone.LeftZoneEvent += SpawnEnemysLeftZone;
    }
    void SpawnEnemysLeftZone()
    {

        for (int i = 0; i < spawnPointsLeftZone.Length; i++)
        {
            EnemyFlyers enemy = EnemyFlyersFactory.instance.GetObjFromPool();
            enemy.transform.position = spawnPointsLeftZone[i].position;
            enemy.transform.parent = _root;
            enemy.isInLeftZone = true;
        }

        AudioManager.instance.Play(AudioManager.Sounds.SpawnEnemies);

    }

    private void OnDestroy()
    {
        LeftZone.LeftZoneEvent -= SpawnEnemysLeftZone;
    }
}
