using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyers : Entity
{
    public static EnemyFlyers instance;
    PatrolStateFlyers _patrol;
    AttackStateFlyers _attack;
    FiniteStateMachineFlyers _stateMachine;
    public int speed;
    public int minDistAttack;
    public Player player;
    public int speedRotation;
    public int shootCooldown;
    public LayerMask _playerMask = 1 << 9;


    // Start is called before the first frame update
    void Start()
    {
        _stateMachine = new FiniteStateMachineFlyers();
        _patrol = new PatrolStateFlyers(this);
        _attack = new AttackStateFlyers(this);

        _stateMachine.AddState(FlyersStates.Patrol, _patrol);
        _stateMachine.AddState(FlyersStates.Attack, _attack);

        _stateMachine.ChangeState(FlyersStates.Patrol);
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.ArtificialUpdate();    
    }

    private void Reset()
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
