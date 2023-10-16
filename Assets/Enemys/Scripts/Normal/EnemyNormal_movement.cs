using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal_movement 
{
    Rigidbody _rb;
    int _speed;
    Player _player;
    Vector3 _dir;
    float _minDistAttacks;
    float _minDist;
    EnemyNormal_Attacks _attacks;

    public EnemyNormal_movement(Rigidbody rb , int speed , Player player , float minDist , EnemyNormal_Attacks attacks , float minDistAttacks)
    {
        _rb = rb;
        _speed = speed;
        _player = player;
        _minDistAttacks = minDistAttacks;
        _attacks = attacks;
        _minDist = minDist;
    }

    public void ArtificialUpdate()
    {
        Move();
        CheckAttack();
    }

    void Move()
    {
        _dir = _player.transform.position - _rb.position;

        var dist = _dir.sqrMagnitude;

        if (dist<=_minDist * _minDist)
        {
            _rb.MovePosition(_rb.position+_dir * _speed * Time.fixedDeltaTime);

        }
    }

    void CheckAttack()
    {
        var distance = _dir.sqrMagnitude;

        if (distance <= _minDistAttacks * _minDistAttacks)
        {
            _attacks.Attack();
        }
    }
}
