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
    int _slimeDmg;
    int _zonesDmg;
    int _twisterDmg;

    public Player_Collisions(Player_Movement movement, Rigidbody rb, Transform spawnpoint, Player Player, Transform transform)
    {

        _movement = movement;
        _rb = rb;
        _checkpoint = spawnpoint;
        _player = Player;
        _transform = transform;
        _slimeDmg = Player.slimeDmg;
        _zonesDmg = Player.zonesDmg;
        _twisterDmg = Player.twisterDmg;
    }

    public void OnCollisionEnter(Collision collision)
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

        if (collision.gameObject.layer == 8)
        {
            _movement.jump = true;
            _transform.parent = collision.transform;
        }

        if (collision.gameObject.layer == 12)
        {
            _player.TakeDmg(3);
        }

        if (collision.gameObject.layer == 13)
        {
            _movement.JumpSlime();
            _player.TakeDmg(_slimeDmg);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            _player.TakeDmg(_zonesDmg);
        }

        if (other.gameObject.layer == 16)
        {
            _movement.Twister();
            _player.TakeDmg(_twisterDmg);
        }
    }


    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
            _movement.jump = false;

        if (collision.gameObject.layer == 8)
            _transform.parent = null;


    }
}
