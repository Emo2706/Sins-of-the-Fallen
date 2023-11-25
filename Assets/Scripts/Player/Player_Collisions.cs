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
    int _circleDmg;
    int _bulletsDmg;
    int _punchDmg;



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
        _circleDmg = Player.circleDmg;
        _bulletsDmg = Player.bulletsDmg;
        _punchDmg = Player.enemyDmg;
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


        if (collision.gameObject.layer == 13)
        {
            _movement.JumpSlime();
            _player.TakeDmg(_slimeDmg);
            
        }

        if (collision.gameObject.layer == 18)
        {
            _player.TakeDmg(_punchDmg);
            
        }

        if (collision.gameObject.layer == 11)
        {
            _player.TakeDmg(_punchDmg);
           

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 14)
        {
            _player.TakeDmg(_zonesDmg);
            _player.ShakeCamera();
        }

        if (other.gameObject.layer == 16)
        {
            _movement.Twister();
            _player.TakeDmg(_twisterDmg);
        }

        if(other.gameObject.layer == 17)
        {
            _player.TakeDmg(_circleDmg);
            
        }

        if (other.gameObject.layer == 12)
        {
            _player.TakeDmg(_bulletsDmg);
            
        }


        var powerUp = other.GetComponent<PowerUp>();

        if (powerUp != null)
            powerUp.Active();
    }


    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
            _movement.jump = false;

        if (collision.gameObject.layer == 8)
            _transform.parent = null;


    }
}
