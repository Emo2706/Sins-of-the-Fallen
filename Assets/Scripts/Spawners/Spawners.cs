using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class Spawners : MonoBehaviour
{
    public EnemyGlobalScript[] enemys;
    public Transform[] spawnPoints;
   // public Dictionary<EnemyGlobalScript, int> enemy_types;

    // Start is called before the first frame update
   protected virtual void Start()
    {
        enemy_types = new Dictionary<EnemyGlobalScript, int>()
        {
            {EnemyFactory.instance.enemies[EnemyFactory.EnemyID.EnemyNormal_ID] , EnemyFactory.EnemyID.EnemyNormal_ID},
            {EnemyFactory.instance.enemies[EnemyFactory.EnemyID.EnemyShooter_ID] , EnemyFactory.EnemyID.EnemyShooter_ID},
            {EnemyFactory.instance.enemies[EnemyFactory.EnemyID.EnemyFlyer_ID] , EnemyFactory.EnemyID.EnemyFlyer_ID}

        };

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            SpawnEnemys(spawnPoints[i].position);
        }
    }

    protected void SpawnEnemys(Vector3 pos)
    {
        /*EnemyGlobalScript enemy = EnemyFactory.instance.GetObjFromPool(enemy_types);
        enemy.transform.position = pos;
        enemy.waypoints = wayPoints;
    }
}*/
