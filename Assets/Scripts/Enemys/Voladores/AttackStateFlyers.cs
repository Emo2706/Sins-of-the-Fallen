using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackStateFlyers : State
{
    EnemyFlyers _flyer;
    int _speedRotation;
    float _shootCooldown;
    Transform _transform;
    float _shootTimer;
    Vector3 _playerPos;
    Player _player;
    Transform _pivotShoot;
    public event Action OnShoot;
    Vector3 _shootDir;

   

    public AttackStateFlyers(EnemyFlyers flyer)
    {
        _flyer = flyer;
        _transform = flyer.transform;
        _shootCooldown = UnityEngine.Random.Range(flyer.shootCooldown, 6);
        _speedRotation = flyer.speedRotation;
        _pivotShoot = flyer.pivotShootFlyer;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Attack");
        _player = _flyer.player;
    }

    public override void OnUpdate()
    {
        if (_flyer.life > 0)
        {
            _playerPos = _player.transform.position - _transform.position;

            _shootDir = _player.transform.position - _pivotShoot.position;

            _transform.forward = _playerPos;

            if (_playerPos.sqrMagnitude >= _flyer.minDistAttack * _flyer.minDistAttack)
            {
                _flyer.ChangeState(FlyersStates.Patrol);
                _flyer.player = null;
            }

            _shootTimer += Time.deltaTime;

            if (_shootTimer >= _shootCooldown)
            {
                Shoot();

                _shootTimer = 0;
            }
        }

        

    }

    public override void OnExit()
    {
        Debug.Log("Exit Attack");
    }

    void Shoot()
    {
        OnShoot();
        var bullet = BulletEnemyFactory.instance.GetObjFromPool();
        bullet.transform.position = _pivotShoot.position;
        bullet.transform.rotation = _transform.rotation;
        bullet.dir = _shootDir.normalized;
        AudioManager.instance.Play(AudioManager.Sounds.EnemyShoot);
    }

    public override void OnFixedUpdate()
    {
        
    }
}
