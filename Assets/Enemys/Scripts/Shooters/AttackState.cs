using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    int _speedRotation;
    int _shootCooldown;
    float _minDistAttack;
    Player _player;
    Transform _transform;
    float _shootTimer;

    public AttackState(EnemyShooter shooter)
    {
        _speedRotation = shooter.speedRotation;
        _shootCooldown = shooter.shootCooldown;
        _minDistAttack = shooter.minDistAttack;
        _player = shooter.player;
        _transform = shooter.transform;
    }

    public override void OnEnter()
    {
        Debug.Log("enter attack");
        var dir = _player.transform.position - _transform.position;
        _transform.forward += dir;
        Shoot();
    }

    public override void OnUpdate()
    {
        var dist = (_player.transform.position - _transform.position).sqrMagnitude;

        if (dist >= _minDistAttack * _minDistAttack)
            fsmSh.ChangeState(ShooterStates.Patrol);

        _shootTimer += Time.deltaTime;

        var dir = _player.transform.position - _transform.position;

        _transform.forward += dir;
        

        if (_shootTimer>= _shootCooldown)
        {
            _shootTimer = 0;

            Shoot();
        }

    }
   
    
    public override void OnExit()
    {
        Debug.Log("exit attack");

    }

    void Shoot()
    {
        var bullet = BulletEnemyFactory.instance.GetObjFromPool();
        bullet.transform.position = _transform.position;
        bullet.transform.rotation = _transform.rotation;
        bullet.dir = _transform.forward;
    }

  


}
