using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    int _speed;
    Transform[] _wayPointsShooter;
    int _indexWayPoint;
    float _minDistAttack;
    Transform _transform;
    Player _player;

    public PatrolState(EnemyShooter shooter)
    {
        _speed = shooter.speed;
        _wayPointsShooter = shooter.waypointsShooter;
        _minDistAttack = shooter.minDistAttack;
        _transform = shooter.transform;
        _player = shooter.player;
    }

    public override void OnEnter()
    {
        Debug.Log("enter patrol");
    }

    //preguntar para que todos tengan distintos waypoints

    public override void OnUpdate()
    {
        var dist = (_player.transform.position - _transform.position).sqrMagnitude;

        if (dist <= _minDistAttack * _minDistAttack)
            fsmSh.ChangeState(ShooterStates.Attack);
    }

    public override void OnExit()
    {
        Debug.Log("exit patrol");
    }

   
}
