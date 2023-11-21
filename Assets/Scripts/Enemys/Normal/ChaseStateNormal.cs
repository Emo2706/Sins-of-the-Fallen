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
    Vector3 dir;
    public event Action<float> OnMovement = delegate { };
    public Action Attack;
    int _maxForce;
    float _separationRadius;
    float _cohesionWeight;
    float _separationWeight;
    float _alignmentWeight;

    public ChaseStateNormal(EnemyNormal enemy)
    {
        _enemy = enemy;
        _transform = enemy.transform;
        _speed = enemy.speed;
        _rb = enemy._rb;
        dir = enemy.dir;
        _separationRadius = enemy.separationRadius;
        _cohesionWeight = enemy.cohesionWeight;
        _separationWeight = enemy.separationWeight;
        _alignmentWeight = enemy.alignmentWeight;
        _maxForce = enemy.maxForce;
    }

    public override void OnEnter()
    {
        Debug.Log("Enter Chase");
    }

    public override void OnUpdate()
    {
        if (_enemy.life>0)
        {
            if (dir.sqrMagnitude >= _enemy.minDist * _enemy.minDist)
                _enemy.ChangeState(NormalStates.Patrol);

            if (dir.sqrMagnitude <= _enemy.minDistAttack * _enemy.minDistAttack)
                Attack();


            Flocking();

        }



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

        _rb.MovePosition(_transform.position + dir * _speed * Time.fixedDeltaTime);

        _transform.forward = dir;

        if (dir.x != 0)
        {
            OnMovement(dir.x);
        }

        if (dir.z != 0)
        {
            OnMovement(dir.z);
        }
    }

    protected Vector3 Seek(Vector3 targetPos, float speed)
    {
        Vector3 desired = targetPos - _transform.position;
        desired.Normalize();
        desired *= speed;

        Vector3 steering = desired - dir;
        steering = Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);
        return steering;
    }


    protected Vector3 Seek(Vector3 seekTarget)
    {
        return Seek(seekTarget, _speed);
    }

    protected Vector3 Arrive(Vector3 targetPos)
    {
        float dist = (targetPos - _transform.position).sqrMagnitude;

        if (dist > _enemy.minDist * _enemy.minDist) return Seek(targetPos);

        return Seek(targetPos, (_speed * (dist / _enemy.minDist)));
    }


    protected void AddForce(Vector3 force)
    {
        dir = Vector3.ClampMagnitude(dir + force, _speed);
    }

    Vector3 CalculateSteering(Vector3 desired)
    {
        Vector3 steering = desired - dir;

        steering = Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);
        return steering;
    }



    protected Vector3 Cohesion(List<EnemyNormal> agents)
    {
        Vector3 desired = Vector3.zero;
        int count = 0;
        foreach (var item in agents)
        {
            if (item == _enemy) continue;
            if ((item.transform.position - _transform.position).sqrMagnitude > _enemy.minDist * _enemy.minDist) continue;

            desired += item.transform.position;
            count++;
        }

        if (count == 0) return Vector3.zero;

        desired /= count;
        return Arrive(desired);
    }

    protected Vector3 Separation(List<EnemyNormal> agents)
    {
        Vector3 desired = Vector3.zero;

        foreach (var item in agents)
        {
            if (item == _enemy) continue;
            Vector3 dist = item.transform.position - _transform.position;

            if (dist.sqrMagnitude > _separationRadius * _separationRadius) continue;

            desired += dist;
        }

        if (desired == Vector3.zero) return desired;

        desired *= -1;

        return CalculateSteering(desired.normalized * _speed);
    }

    protected Vector3 Alignment(List<EnemyNormal> agents)
    {
        Vector3 desired = Vector3.zero;
        int count = 0;

        foreach (var item in agents)
        {
            if (item == _enemy) continue;
            if ((item.transform.position - _transform.position).sqrMagnitude > _enemy.minDist * _enemy.minDist) continue;

            desired += item.dir;
            count++;
        }

        if (count == 0) return Vector3.zero;

        desired /= count;

        return CalculateSteering(desired.normalized * _speed);
    }

    void Flocking()
    {
        AddForce(Cohesion(GameManager.instance.enemyNormals) * _cohesionWeight + Separation(GameManager.instance.enemyNormals) * _separationWeight + Alignment(GameManager.instance.enemyNormals) * _alignmentWeight);
    }

}
