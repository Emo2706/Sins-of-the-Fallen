using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGlobalScript : Entity
{
    public Rigidbody _rb;
    Enemy_Collisions _collisions;
    public GameObject _parent;
    public int bulletsDmg;
    // Start is called before the first frame update
   protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        life = _maxLife;
    }


    public void DisableOnDead()
    {
        enabled = false;
    }


}
