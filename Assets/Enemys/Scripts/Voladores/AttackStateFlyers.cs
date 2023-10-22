using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateFlyers : State
{
    EnemyFlyers _flyer;
    int _speedRotation;
    int _shootCooldown;
    float _minDistAttack;
    Transform _transform;
    float _shootTimer;

    public AttackStateFlyers(EnemyFlyers flyer)
    {
        _flyer = flyer;
        _transform = flyer.transform;
        _minDistAttack = flyer.minDistAttack;
        _shootCooldown = flyer.shootCooldown;
        _speedRotation = flyer.speedRotation;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Attack");
    }

    public override void OnUpdate()
    {
        var dir = _flyer.player.transform.position - _transform.position;

        _transform.forward = dir;

        if (dir.sqrMagnitude >= _minDistAttack * _minDistAttack)
        {
            fsmFly.ChangeState(FlyersStates.Patrol);
        }

        _shootTimer += Time.deltaTime;

        if(_shootTimer >= _shootCooldown)
        {
            Shoot();

            _shootTimer = 0;
        }

    }

    public override void OnExit()
    {
        Debug.Log("Exit Attack");
    }

    void Shoot()
    {
        var bullet = BulletEnemyFactory.instance.GetObjFromPool();
        bullet.transform.position = _transform.position;
        bullet.transform.rotation = _transform.rotation;
        bullet.dir = _transform.forward;
    }

}
