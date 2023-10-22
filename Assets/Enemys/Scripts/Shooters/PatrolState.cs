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
    LayerMask _playerMask;
    EnemyShooter _shooter;

    public PatrolState(EnemyShooter shooter)
    {
        _shooter = shooter;
        _speed = shooter.speed;
        _wayPointsShooter = shooter.waypointsShooter;
        _minDistAttack = shooter.minDistAttack;
        _transform = shooter.transform;
        _playerMask = shooter.playerMask;
    }

    public override void OnEnter()
    {
        Debug.Log("enter patrol");
    }

    //preguntar para que todos tengan distintos waypoints

    public override void OnUpdate()
    {
        var player = Physics.OverlapSphere(_transform.position, _minDistAttack,_playerMask);

        foreach (var item in player)
        {
            if (item.GetComponent<Player>() != null)
            {
                _shooter.player = item.GetComponent<Player>();
                fsmSh.ChangeState(ShooterStates.Attack);
            } 
        }
    }

    public override void OnExit()
    {
        Debug.Log("exit patrol");
    }

   
}
