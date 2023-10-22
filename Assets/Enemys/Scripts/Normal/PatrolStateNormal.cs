using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolStateNormal : State
{
    EnemyNormal _enemy;
    Transform _transform;
    float _minDist;
    LayerMask _playerMask;
    int _speed;

    public PatrolStateNormal (EnemyNormal enemy)
    {
        _enemy = enemy;
        _transform = enemy.transform;
        _minDist = enemy.minDist;
        _playerMask = enemy.playerMask;
        _speed = enemy.speed;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Patrol");
    }

    public override void OnUpdate()
    {
        // preguntar por distintos waypoints para los enemigos

        var player = Physics.OverlapSphere(_transform.position, _minDist, _playerMask);


        foreach (var item in player)
        {
            if(item.GetComponent<Player>() != null)
            {
                _enemy.player = item.GetComponent<Player>();

                fsmNm.ChangeState(NormalStates.Chase);
            }
        }

    }
    
    public override void OnExit()
    {
        Debug.Log("Exit Patrol");
    }

}
