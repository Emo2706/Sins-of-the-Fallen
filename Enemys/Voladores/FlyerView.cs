using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerView
{
    Animator _anim;
    EnemyFlyers _flyer;

    public FlyerView(EnemyFlyers flyer)
    {
        _anim = flyer.GetComponentInChildren<Animator>();
        _flyer = flyer;
    }
    public void Shoot()
    {
        if(_flyer.life>0)
        _anim.SetTrigger("Shoot");
    }

    public void Dead()
    {
        _anim.SetTrigger("Die");
    }
}
