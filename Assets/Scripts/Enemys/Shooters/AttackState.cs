using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackState : State
{
    int _speedRotation;
    int _shootCooldown;
    float _minDistAttack;
    Transform _transform;
    float _shootTimer;
    EnemyShooter _shooter;
    Vector3 _playerPos;
    Player _player;
    public event Action OnShoot;

    public AttackState(EnemyShooter shooter)
    {
        _shooter = shooter;
        _speedRotation = shooter.speedRotation;
        _shootCooldown = shooter.shootCooldown;
        _minDistAttack = shooter.minDistAttack;
        _transform = shooter.transform;
    }

    public override void OnEnter()
    {
        Debug.Log("enter attack");
        _player = _shooter.player;
    }

    public override void OnUpdate()
        
    {
        _playerPos = _player.transform.position - _transform.position;

        if (_playerPos.sqrMagnitude >= _minDistAttack * _minDistAttack)
        {
            _shooter.ChangeState(ShooterStates.Patrol);
            _shooter.player = null;
        }
            

        _shootTimer += Time.deltaTime;


        _transform.forward = _playerPos;
        

        if (_shootTimer>= _shootCooldown)
        {
            Shoot();
            
            _shootTimer = 0;

        }

    }
   
    
    public override void OnExit()
    {
        Debug.Log("exit attack");

    }


    public override void OnFixedUpdate()
    {
      
    }

    void Shoot()
    {
        var bullet = BulletEnemyFactory.instance.GetObjFromPool();
        bullet.transform.position = _transform.position;
        bullet.transform.rotation = _transform.rotation;
        bullet.dir = _transform.forward;
        OnShoot();
    }
}
