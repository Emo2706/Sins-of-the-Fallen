using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : State
{
    int _shootCoolDown;
    public Player player;
    Transform _transform;
    float _viewRadius;
    LayerMask _playerMask;
    float _shootTimer;
    int _changeStateCooldown;
    float _changeStateTimer;
    Boss _boss;
    int _circleCooldown;
    Transform _pivotShoot;
    public ShootState(Boss boss)
    {
        _boss = boss;
        _shootCoolDown = boss.coolDownShoot;
        _transform = boss.transform;
        _viewRadius = boss.viewRadius;
        _playerMask = boss.playerMask;
        _changeStateCooldown = boss.coolDownChangeAttacks;
        _circleCooldown = boss.coolDownCircle;
        _pivotShoot = boss.pivotShoot;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter shoot");
        _changeStateTimer = 0;
        _shootTimer = 0;
    }

    public override void OnExit()
    {
        Debug.Log("Exit shoot");
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnUpdate()
    {
       

        _shootTimer += Time.deltaTime;

        var dir = player.transform.position - _transform.position;

        _transform.forward += dir;
        if (dir.sqrMagnitude <= _viewRadius * _viewRadius)
        {
            if(_shootTimer >= _shootCoolDown)
            {
                Shoot();

                _shootTimer = 0;
            }

        }

        _changeStateTimer += Time.deltaTime;

        if(_changeStateTimer>= _changeStateCooldown)
        {
            _boss.ChangeState(BossStates.Zones);
        }

    }

    void Shoot()
    {
        var bullet = BulletBossFactory.instance.GetObjFromPool();
        bullet.transform.position = _pivotShoot.position;
        bullet.transform.forward =  _transform.forward;
    }
}
