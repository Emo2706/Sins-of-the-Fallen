using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal_movement 
{
    Rigidbody _rb;
    int _speed;
    Player _player;
    Vector3 _dir;
    float _minDist;
    EnemyNormal_Attacks _attacks;

    public EnemyNormal_movement(Rigidbody rb , int speed , Player player , float minDist , EnemyNormal_Attacks attacks)
    {
        _rb = rb;
        _speed = speed;
        _player = player;
        _minDist = minDist;
        _attacks = attacks;
    }

    public void ArtificialUpdate()
    {
        Move();
        CheckAttack();
    }

    void Move()
    {
        _dir = _player.transform.position - _rb.position;

        _rb.MovePosition(_rb.position+_dir * _speed * Time.deltaTime);

        
    }

    void CheckAttack()
    {
        var distance = _dir.sqrMagnitude;

        if (distance <= _minDist)
        {
            _attacks.Attack();
        }
    }
}
