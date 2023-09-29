using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : Entity
{
    public static EnemyNormal instance;
    EnemyNormal_movement _movement;
    EnemyNormal_Attacks _attacks;
    Rigidbody _rb;
    [SerializeField] int _speed;
    [SerializeField] float _minDist;
    [SerializeField] Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _attacks = new EnemyNormal_Attacks();
        _movement = new EnemyNormal_movement(_rb ,_speed , _player , _minDist , _attacks);
    }

    // Update is called once per frame
    void Update()
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

    public override void CheckLife()
    {
        base.CheckLife();

        if (_life<=0)
        {
            EnemyFactory.instance.ReturnToPool(this);
        }
    }
}
