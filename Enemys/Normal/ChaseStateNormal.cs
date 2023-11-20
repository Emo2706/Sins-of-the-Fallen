using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChaseStateNormal : State
{
    EnemyNormal _enemy;
    Transform _transform;
    int _speed;
    Rigidbody _rb;
    float _minDist;
    float _minDistAttack;
    Vector3 dir;
    public event Action<float> OnMovement = delegate { };
    public Action Attack;

    public ChaseStateNormal(EnemyNormal enemy)
    {
        _enemy = enemy;
        _transform = enemy.transform;
        _speed = enemy.speed;
        _rb = enemy._rb;
        _minDist = enemy.minDist;
        _minDistAttack = enemy.minDistAttack;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Chase");
    }

    public override void OnUpdate()
    {
        if (dir.sqrMagnitude >= _minDist * _minDist)
            _enemy.ChangeState(NormalStates.Patrol);

        if (dir.sqrMagnitude <= _minDistAttack * _minDist)
            Attack();

    }


    public override void OnExit()
    {
        Debug.Log("Exit Chase");
    }

    public override void OnFixedUpdate()
    {
        if(_enemy.life>0)
        Move();
    }

    void Move()
    {
        dir = _enemy.player.transform.position - _transform.position;

        _transform.forward += dir;

        _rb.MovePosition(_transform.position + dir * _speed * Time.deltaTime);

        if (dir.x != 0)
        {
            OnMovement(dir.x);
        }

        if (dir.z != 0)
        {
            OnMovement(dir.z);
        }
    }




}
