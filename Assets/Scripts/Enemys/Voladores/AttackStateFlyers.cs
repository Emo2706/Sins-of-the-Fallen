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
    Vector3 _playerPos;
    Player _player;
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
        _player = _flyer.player;
    }

    public override void OnUpdate()
    {
        _playerPos = _player.transform.position - _transform.position;

        _transform.forward = _playerPos;

        if (_playerPos.sqrMagnitude >= _minDistAttack * _minDistAttack)
        {
            _flyer.ChangeState(FlyersStates.Patrol);
            _flyer.player = null;
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

    public override void OnFixedUpdate()
    {
        
    }
}
