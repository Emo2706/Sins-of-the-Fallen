using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalView
{
    Animator _anim;

    public NormalView(EnemyNormal enemy)
    {
        _anim = enemy.GetComponentInChildren<Animator>();
    }

    public void SetX(float x)
    {
        _anim.SetFloat("xAxis", x);
    }

    public void SetZ(float z)
    {
        _anim.SetFloat("zAxis", z);
    }

    public void Punch()
    {
        _anim.SetTrigger("Punch");
    }

    public void Die()
    {
        _anim.SetTrigger("Die");
    }
}
