using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    // Tiene tener referencias de todos los spawnpoints y una vez que lo spawnea le paso los spawnpoints igualandolos

    [SerializeField] Transform[] _spawnPointsTwister;
    [SerializeField] Transform[] _spawnPointsZones;
    [SerializeField] Transform[] _spawnPointsLifePotions;
    [SerializeField] Transform[] _spawnPointsTargetsShield;

    [SerializeField] Transform _spawnPointBoss;

    private void Start()
    {
        var boss= BossFactory.instance.GetObjFromPool();
        boss.transform.position = _spawnPointBoss.position;
        boss.spawnPointsPotions = _spawnPointsLifePotions;
        boss.spawnPointsTwister = _spawnPointsTwister;
        boss.spawnPointsZone = _spawnPointsZones;
        boss.spawnPointsTargetsShield = _spawnPointsTargetsShield;
    }
}
