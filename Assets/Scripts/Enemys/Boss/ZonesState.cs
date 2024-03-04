using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZonesState : State
{
    int _zoneAttackCooldown;
    ZoneAttackWarning _warning;
    int _changeStateCooldown;
    Boss _boss;
    public event Action Zone = delegate { };

    public ZonesState(Boss boss)
    {
        _boss = boss;
        _zoneAttackCooldown = boss.zoneAttackCooldown;
        _changeStateCooldown = boss.cooldownChangeAttackCircle;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter zones");

        _warning = ZoneAttackWarningFactory.instance.GetObjFromPool();
        _warning.transform.position = _boss.spawnPointsZone[UnityEngine.Random.Range(0, _boss.spawnPointsZone.Length)].position;
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
        Vector3 warningpos = _warning.transform.position - _boss.transform.position;

        warningpos.y = 0;

        Quaternion warningRotation = Quaternion.LookRotation(warningpos, Vector3.up);

        _boss.transform.rotation = Quaternion.Euler(0, warningRotation.eulerAngles.y, 0);
    }

    IEnumerator ZoneAttackCouroutine()
    {
        yield return new WaitUntil (()=>_boss.enabled);

        WaitForSeconds _waitForSeconds = new WaitForSeconds(_zoneAttackCooldown);

        WaitForSeconds _waitForChangeState = new WaitForSeconds(_changeStateCooldown);

        yield return _waitForSeconds;

        yield return new WaitUntil(() => _boss.enabled);


        ZoneAttack zoneAttack = ZoneAttackFactory.instance.GetObjFromPool();
        zoneAttack.transform.position = _warning.transform.position;
        Zone();
        AudioManager.instance.Play(AudioManager.Sounds.Zones);

        yield return _waitForChangeState;

        _boss.RandomChangeState();
    }
   
}
