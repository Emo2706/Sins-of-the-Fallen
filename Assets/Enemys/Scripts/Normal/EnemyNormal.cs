using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyNormal : Entity
{
    public static EnemyNormal instance;
    EnemyNormal_movement _movement;
    EnemyNormal_Attacks _attacks;
    Rigidbody _rb;
    [SerializeField] int _speed;
    [SerializeField] float _minDist;
    [SerializeField] Player _player;
    [SerializeField] float _minDistAttack;
    public event Action<float> OnMovement = delegate { };
    public event Action<float> OnLifeChange = delegate { };
    public event Action OnJump = delegate { };
    public event Action OnDead = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _rb = GetComponent<Rigidbody>();
        _attacks = new EnemyNormal_Attacks();
        _movement = new EnemyNormal_movement(_rb ,_speed , _player , _minDist , _attacks , _minDistAttack);
    }

   

    private void FixedUpdate()
    {
        _movement.ArtificialUpdate();
        
    }

    private void Reset()
    {
        _life = _maxLife;
    }


    public static void TurnOnCallBack(EnemyNormal enemy)
    {
        enemy.Reset();
        enemy.gameObject.SetActive(true);
    }
    public static void TurnOffCallBack(EnemyNormal enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    public override void TakeDmg(int dmg)
    {
        base.TakeDmg(dmg);

        CheckLife();
    }

    void CheckLife()
    {
        if (_life <= 0)
            EnemyFactory.instance.ReturnToPool(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _minDist);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _minDistAttack);
    }
}
