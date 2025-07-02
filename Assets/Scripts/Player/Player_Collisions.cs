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
    EnemyNormal _enemy;
    int _haiserForce;
    int _slimeDmg;
    int _zonesDmg;
    int _twisterDmg;
    int _circleDmg;
    int _bulletsDmg;
    int _punchDmg;
    int _lavaDmg;
    int _lavaCooldown;
    LifeHandler _lifeHandler;


    public Player_Collisions(Player_Movement movement, Rigidbody rb, Transform spawnpoint, Player Player, Transform transform, LifeHandler lifeHandler)
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
        _lifeHandler = lifeHandler;
        _haiserForce = Player.haiserForce;
        _lavaDmg = Player.lavaDmg;
        _lavaCooldown = Player.lavaCooldown;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _movement.jump = true;
            _movement._canGlide = false;
            _movement.firstGlideEntrance = true;
            _rb.drag = _initialDrag;
            AudioManager.instance.Stop(AudioManager.Sounds.Glide);
        }
        if (collision.gameObject.layer == 7)
        {
            _player.StartCoroutine(DmgLava(_lavaCooldown, _lavaDmg));
            _movement.jump = true;
            _movement._canGlide = false;
            _movement.firstGlideEntrance = true;
            AudioManager.instance.Stop(AudioManager.Sounds.Glide);
        }

        if (collision.gameObject.layer == 8)
        {
            _movement.jump = true;
            _movement._canGlide = false;
            _movement.firstGlideEntrance = true;
            _transform.parent = collision.transform;
            AudioManager.instance.Stop(AudioManager.Sounds.Glide);
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
            _enemy = collision.gameObject.GetComponent<EnemyNormal>();
            if (_enemy != null)
            {
                if (!_enemy.dead)
                {
                    _player.TakeDmg(_punchDmg);
                }
            }
        }
        var haiser = collision.gameObject.GetComponent<GlideHaiser>();

        if (haiser != null)
        {
            if (haiser.activate)
            {
                _rb.AddForce(Vector3.up * _haiserForce, ForceMode.Impulse);
                _movement.jump = false;
                _player.StartCoroutine(_movement.GlideEnable());
            }
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

            var twister = other.gameObject.GetComponent<TwisterAttack>();

            if (twister != null) twister.Deactivate();
        }

        if (other.gameObject.layer == 17)
        {
            _player.TakeDmg(_circleDmg);
        }

        if (other.gameObject.layer == 12)
        {
            _player.TakeDmg(_bulletsDmg);

        }

        if (other.gameObject.layer == 23)
        {
            CheckPointManager.instance.SetSpawnPosition(other.transform.position);
        }


        var powerUp = other.GetComponent<PowerUp>();

        if (powerUp != null && _player.life < 50)
            powerUp.Active();
    }


    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
            _movement.jump = false;

        if (collision.gameObject.layer == 8)
            _transform.parent = null;

        if (collision.gameObject.layer == 7)
            _player.StartCoroutine(DmgLava(_lavaCooldown , _lavaDmg));
    }

    IEnumerator DmgLava(int lavaCooldown, int lavaDmg)
    {
        WaitUntil pause = new WaitUntil(() => !GameManager.instance.pause);

        WaitForSeconds wait = new WaitForSeconds(_lavaCooldown);

        while (true)
        {
            yield return pause;

            _player.TakeDmg(_lavaDmg);

            yield return wait;
        }
    }
}
