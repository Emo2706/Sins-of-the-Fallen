using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterView
{
    Animator _anim;
    EnemyShooter _shooter;

    public ShooterView(EnemyShooter shooter)
    {
        _shooter = shooter;
        _anim = shooter.GetComponentInChildren<Animator>();
    }

    public void Shoot()
    {
        if(_shooter.life>0)
        _anim.SetTrigger("Shoot");
    }

    public void Die()
    {
        _anim.SetTrigger("Die");
    }
}
