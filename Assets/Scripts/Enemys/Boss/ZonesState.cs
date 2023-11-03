using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesState : State
{
    int _zoneAttackCooldown;
    Transform[] _spawnPointsZones;
    ZoneAttackWarning warning;
    int _changeStateCooldown;
    Boss _boss;
    public ZonesState(Boss boss)
    {
        _boss = boss;
        _zoneAttackCooldown = boss.zoneAttackCooldown;
        _spawnPointsZones = boss.spawnPointsZone;
        _changeStateCooldown = boss.cooldownChangeAttackCircle;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter zones");

        warning = ZoneAttackWarningFactory.instance.GetObjFromPool();
        warning.transform.position = _spawnPointsZones[Random.Range(0, _spawnPointsZones.Length)].position;
        _boss.StartCoroutine(ZoneAttackCouroutine());
    }

    public override void OnExit()
    {
        Debug.Log("Exit zones");

    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnUpdate()
    {
        
    }

    IEnumerator ZoneAttackCouroutine()
    {
        WaitForSeconds _waitForSeconds = new WaitForSeconds(_zoneAttackCooldown);

        WaitForSeconds _waitForChangeState = new WaitForSeconds(_changeStateCooldown);

        yield return _waitForSeconds;

        ZoneAttack zoneAttack = ZoneAttackFactory.instance.GetObjFromPool();
        zoneAttack.transform.position = warning.transform.position;

        yield return _waitForChangeState;

        _boss.RandomChangeState();
    }
   
}
