using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    float _minDistAttack;
    Transform _transform;
    LayerMask _playerMask;
    EnemyShooter _shooter;

    public PatrolState(EnemyShooter shooter)
    {
        _shooter = shooter;
        _minDistAttack = shooter.minDistAttack;
        _transform = shooter.transform;
        _playerMask = shooter.playerMask;
    }

    public override void OnEnter()
    {
        Debug.Log("enter patrol");
    }


    public override void OnUpdate()
    {
        if (_shooter.player == null)
        {
            var player = Physics.OverlapSphere(_transform.position, _minDistAttack,_playerMask);

            foreach (var item in player)
            {
                if (item.GetComponent<Player>() != null)
                {
                    _shooter.player = item.GetComponent<Player>();
                    _shooter.ChangeState(ShooterStates.Attack);
                } 
            }

        }

    }

    public override void OnExit()
    {
        Debug.Log("exit patrol");
    }

    public override void OnFixedUpdate()
    {
        
    }
}
