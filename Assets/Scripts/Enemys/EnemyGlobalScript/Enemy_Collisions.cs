using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Collisions
{
    EnemyGlobalScript _enemy;
    Rigidbody _rb;

    public Enemy_Collisions(EnemyGlobalScript enemy , Rigidbody rb)
    {
        _enemy = enemy;
        _rb = rb;
    }

   public void ArtificialOnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
            _enemy.TakeDmg(5);
    }
}
