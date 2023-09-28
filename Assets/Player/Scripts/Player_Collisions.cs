using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collisions 
{
    Player_Movement _movement;
    Rigidbody _rb;
    float _initialDrag = 0.05f;
    Transform _checkpoint;
    Player _player;

    public Player_Collisions(Player_Movement movement , Rigidbody rb, Transform spawnpoint, Player Player)
    {
        _movement = movement;
        _rb = rb;
        _checkpoint = spawnpoint;
        _player = Player;
    }

    public void ArtificialOnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _movement.jump = true;
            _rb.drag = _initialDrag;

        }
        if (collision.gameObject.layer == 7)
        {
            _player.gameObject.transform.position = _checkpoint.position;
        }


        
    }

    public void ArtificialOnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
            _movement.jump = false;


    }
}
