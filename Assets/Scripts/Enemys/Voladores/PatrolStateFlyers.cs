using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStateFlyers : State
{
    EnemyFlyers _flyer;
    int _speed;
    Transform _transform;
    float _minDistAttack;
    LayerMask _playerMask;


    public PatrolStateFlyers(EnemyFlyers flyer)
    {
        _flyer = flyer;
        _speed = flyer.speed;
        _transform = flyer.transform;
        _minDistAttack = flyer.minDistAttack;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Patrol");
    }

    public override void OnUpdate()
    {
       

        var player = Physics.OverlapSphere(_transform.position, _minDistAttack, _playerMask);

        foreach (var item in player)
        {
            _flyer.player = item.GetComponent<Player>();

            _flyer.ChangeState(FlyersStates.Attack);
        }
    }

    public override void OnExit()
    {
        Debug.Log("Exit Patrol");
    }

    public override void OnFixedUpdate()
    {
        // preguntar por distintos waypoints para los enemigos
    }
}
