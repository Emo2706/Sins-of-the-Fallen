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
    [SerializeField] Transform _root;
    [SerializeField] Transform _spawnPointCircleAttack;

    [SerializeField] Transform _spawnPointBoss;
    [SerializeField] List<GameObject> _beams;
    [SerializeField] BossBar _bossBar;

    private void Start()
    {
        Songs.OnEnterBossZone += SpawnBoss;
    }

    void SpawnBoss()
    {
        var boss = BossFactory.instance.GetObjFromPool();
        _bossBar.boss = boss;
        boss.transform.position = _spawnPointBoss.position;
        boss.spawnPointsPotions = _spawnPointsLifePotions;
        boss.spawnPointsTwister = _spawnPointsTwister;
        boss.spawnPointsZone = _spawnPointsZones;
        boss.spawnPointsTargetsShield = _spawnPointsTargetsShield;
        boss.spawnPointCircle = _spawnPointCircleAttack;
        boss.transform.parent = _root;
        boss.shieldBeams = _beams;
    }

    private void OnDestroy()
    {
        Songs.OnEnterBossZone -= SpawnBoss;
    }
}
