using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesState : State
{
    int _zoneAttackCooldown;
    Transform[] _spawnPointsZones;
    ZoneAttackWarning warning;
    int _changeStateCooldown;

    public ZonesState(Boss boss)
    {
        _zoneAttackCooldown = boss.zoneAttackCooldown;
        _spawnPointsZones = boss.spawnPointsZone;
        _changeStateCooldown = boss.coolDownChangeAttacks;
    }

    public override void OnEnter()
    {
        warning = ZoneAttackWarningFactory.instance.GetObjFromPool();
        warning.transform.position = _spawnPointsZones[Random.Range(1, 3)].position;
            
    }

    public override void OnExit()
    {
        
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
    }
   
}
