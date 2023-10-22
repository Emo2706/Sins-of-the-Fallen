using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseStateNormal : State
{
    EnemyNormal _enemy;
    Transform _transform;
    int _speed;
    Rigidbody _rb;
    float _minDist;
    float _minDistAttack;

    public ChaseStateNormal(EnemyNormal enemy)
    {
        _enemy = enemy;
        _transform = enemy.transform;
        _speed = enemy.speed;
        _rb = enemy.rb;
        _minDist = enemy.minDist;
        _minDistAttack = enemy.minDistAttack;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Chase");
    }

    public override void OnUpdate()
    {
        var dir = _enemy.player.transform.position - _transform.position;

        _transform.forward += dir;

        _rb.MovePosition(_transform.position + dir * _speed * Time.deltaTime);

        if (dir.sqrMagnitude >= _minDist * _minDist)
            fsmNm.ChangeState(NormalStates.Patrol);

    }


    public override void OnExit()
    {
        Debug.Log("Exit Chase");
    }



}
