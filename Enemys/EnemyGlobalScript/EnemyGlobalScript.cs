using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyGlobalScript : Entity
{
    public Rigidbody _rb;
    public int bulletsDmg;
    public float dieAnimationDuration;
    // Start is called before the first frame update
   protected virtual void Start()
    {
        life = _maxLife;
    }




    public void DisableOnDead()
    {
        enabled = false;
    }

   

    
}
