using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyers : EnemyGlobalScript
{
    public static EnemyFlyers instance;
    PatrolStateFlyers _patrol;
    AttackStateFlyers _attack;
    FSM<FlyersStates> _stateMachine;
    public int speed;
    public int minDistAttack;
    public Player player;
    public int speedRotation;
    public int shootCooldown;
    public LayerMask _playerMask = 1 << 9;
    // Start is called before the first frame update
   protected override void Start()
    {
        base.Start();

        _stateMachine = new FSM<FlyersStates>();
        _patrol = new PatrolStateFlyers(this);
        _attack = new AttackStateFlyers(this);

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
        _life = _maxLife;
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
        if (_life <= 0)
            EnemyFlyersFactory.instance.ReturnToPool(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;

        Gizmos.DrawWireSphere(transform.position, minDistAttack);
    }
}

public enum FlyersStates
{
    Patrol,
    Attack
}