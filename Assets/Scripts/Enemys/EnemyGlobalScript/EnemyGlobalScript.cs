using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGlobalScript : Entity
{
    //public Transform[] waypoints;
    public Rigidbody _rb;
    Enemy_Collisions _collisions;

    // Start is called before the first frame update
   protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _life = _maxLife;
        _collisions = new Enemy_Collisions(this, _rb);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collisions.ArtificialOnCollisionEnter(collision);
    }


}
