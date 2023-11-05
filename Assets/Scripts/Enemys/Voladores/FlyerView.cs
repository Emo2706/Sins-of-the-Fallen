using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerView
{
    Animator _anim;

    public FlyerView(EnemyFlyers flyer)
    {
        _anim = flyer.GetComponentInChildren<Animator>();
    }
    public void Shoot()
    {
        _anim.SetTrigger("Shoot");
    }

    public void Dead()
    {
        _anim.SetTrigger("Die");
    }
}
