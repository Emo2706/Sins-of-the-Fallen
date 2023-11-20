using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStateNormal : State
{
    EnemyNormal _enemy;
    Transform _transform;
    LayerMask _playerMask;
    int _speed;

    public PatrolStateNormal (EnemyNormal enemy)
    {
        _enemy = enemy;
        _transform = enemy.transform;
        _playerMask = enemy.playerMask;
        _speed = enemy.speed;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Patrol");
    }

    public override void OnUpdate()
    {

        var player = Physics.OverlapSphere(_transform.position, _enemy.minDist, _playerMask);


        foreach (var item in player)
        {
            if(item.GetComponent<Player>() != null)
            {
                _enemy.player = item.GetComponent<Player>();

                _enemy.ChangeState(NormalStates.Chase);
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
