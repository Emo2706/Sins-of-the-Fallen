using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collisions 
{
    Player_Movement _movement;
    Rigidbody _rb;
    float _initialDrag = 0.1f;

    public Player_Collisions(Player_Movement movement , Rigidbody rb)
    {
        _movement = movement;
        _rb = rb;
    }

    public void ArtificialOnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _movement.jump = true;
            _rb.drag = _initialDrag;

        }
        
    }

    public void ArtificialOnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
            _movement.jump = false;


    }
}
