using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalView
{
    Animator _anim;
    EnemyNormal _enemy;

    public NormalView(EnemyNormal enemy)
    {
        _enemy = enemy;
        _anim = enemy.GetComponentInChildren<Animator>();
    }

    public void SetX(float x)
    {
        if (_enemy.life > 0)
            _anim.SetFloat("xAxis", x);
            
    }

    public void SetZ(float z)
    {
        if (_enemy.life > 0)
            _anim.SetFloat("zAxis", z);
    }

    public void Punch()
    {
        if(_enemy.life>0 && _enemy.minDistAttack * _enemy.minDistAttack > _enemy.dir.sqrMagnitude)
        _anim.SetTrigger("Punch");
    }

    public void Die()
    {
        _anim.SetFloat("xAxis", 0);
        _anim.SetFloat("zAxis", 0);
        _anim.SetTrigger("Die");
        _anim.Play("Dying");
    }
}
