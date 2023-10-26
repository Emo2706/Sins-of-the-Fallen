using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    int _speedRotation;
    int _shootCooldown;
    float _minDistAttack;
    Transform _transform;
    float _shootTimer;
    EnemyShooter _shooter;

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
    }

    public override void OnUpdate()
        
    {
        var dir = _shooter.player.transform.position - _transform.position;

        if (dir.sqrMagnitude >= _minDistAttack * _minDistAttack)
            _shooter.ChangeState(ShooterStates.Patrol);

        _shootTimer += Time.deltaTime;


        _transform.forward += dir;
        

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
    }
}
