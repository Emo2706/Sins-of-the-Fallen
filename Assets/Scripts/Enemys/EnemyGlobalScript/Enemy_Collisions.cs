using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Collisions
{
    EnemyGlobalScript _enemy;
    Rigidbody _rb;
    int _bulletsDmg;
    public Enemy_Collisions(EnemyGlobalScript enemy , Rigidbody rb)
    {
        _enemy = enemy;
        _rb = rb;
        _bulletsDmg = enemy.bulletsDmg;
    }
}
