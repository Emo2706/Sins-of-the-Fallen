using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStateFlyers : State
{
    EnemyFlyers _flyer;
    int _speed;
    Transform _transform;
    LayerMask _playerMask;


    public PatrolStateFlyers(EnemyFlyers flyer)
    {
        _flyer = flyer;
        _speed = flyer.speed;
        _transform = flyer.transform;
        _playerMask = flyer.playerMask;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Patrol");
    }

    public override void OnUpdate()
    {

        if (_flyer.player == null)
        {
            var player = Physics.OverlapSphere(_transform.position, _flyer.minDistAttack, _playerMask);

            foreach (var item in player)
            {
                _flyer.player = item.GetComponent<Player>();

                _flyer.ChangeState(FlyersStates.Attack);
            }
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
