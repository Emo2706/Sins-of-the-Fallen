using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyFlyers : EnemyGlobalScript
{
    public static EnemyFlyers instance;
    PatrolStateFlyers _patrol;
    AttackStateFlyers _attack;
    FlyerView _view;
    FSM<FlyersStates> _stateMachine;
    public int speed;
    public int minDistAttack;
    public Player player;
    public int speedRotation;
    public float shootCooldown;
    public LayerMask playerMask = 1 << 9;
    public Transform pivotShootFlyer;
    public event Action OnDie;
    // Start is called before the first frame update
   protected override void Start()
    {
        base.Start();

        _stateMachine = new FSM<FlyersStates>();
        _patrol = new PatrolStateFlyers(this);
        _attack = new AttackStateFlyers(this);
        _view = new FlyerView(this);

        _attack.OnShoot += _view.Shoot;
        OnDie += _view.Dead;

        _stateMachine.AddState(FlyersStates.Patrol, _patrol);
        _stateMachine.AddState(FlyersStates.Attack, _attack);

        ChangeState(FlyersStates.Patrol);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void ChangeState(FlyersStates state) => _stateMachine.ChangeState(state);

    public void Reset()
    {
        life = _maxLife;
    }

    public static void TurnOnCallBack(EnemyFlyers enemy)
    {
        enemy.Reset();
        enemy.gameObject.SetActive(true);
    }

    public static void TurnOffCallBack(EnemyFlyers enemy)
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
        if (life <= 0)
            StartCoroutine(DieCoroutine());
    }

    public IEnumerator DieCoroutine()
    {
        WaitForSeconds dieAnimation = new WaitForSeconds(dieAnimationDuration);

        var lifePotion = UnityEngine.Random.Range(1, 3);

        OnDie();

        yield return dieAnimation;

        if (lifePotion == 2)
        {
            var potion = LifePotionFactory.instance.GetObjFromPool();
            potion.transform.position = transform.position;
            AudioManager.instance.Play(AudioManager.Sounds.InstancePowerUp);
        }

        EnemyFlyersFactory.instance.ReturnToPool(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(transform.position, minDistAttack);
    }
}

public enum FlyersStates
{
    Patrol,
    Attack
}
