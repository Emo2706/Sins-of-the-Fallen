using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] Transform _spawnPointBoss;

    private void Start()
    {
        var boss= BossFactory.instance.GetObjFromPool();
        boss.transform.position = _spawnPointBoss.position; 
    }
}
