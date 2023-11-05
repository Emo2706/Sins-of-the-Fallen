using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterView 
{
    Animator _anim;

    public ShooterView(EnemyShooter shooter)
    {
        _anim = shooter.GetComponentInChildren<Animator>();
    }

    public void Shoot()
    {
        _anim.SetTrigger("Shoot");
    }

    public void Die()
    {
        _anim.SetTrigger("Die");
    }
}
