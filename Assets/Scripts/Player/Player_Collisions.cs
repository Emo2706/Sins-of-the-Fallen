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
    Transform _transform;
 
    public Player_Collisions(Player_Movement movement , Rigidbody rb, Transform spawnpoint, Player Player , Transform transform)
    {

        _movement = movement;
        _rb = rb;
        _checkpoint = spawnpoint;
        _player = Player;
        _transform = transform;
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

        if (collision.gameObject.layer==8)
        {
            _movement.jump = true;
            _transform.parent = collision.transform;
        }

        if (collision.gameObject.layer==12)
        {
            _player.TakeDmg(3);
        }

        if(collision.gameObject.layer == 13)
        {
            _movement.JumpSlime();
            _player.TakeDmg(15);
        }
        
    }

    public void ArtificialOnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
            _movement.jump = false;

        if (collision.gameObject.layer == 8)
            _transform.parent = null;


    }
}