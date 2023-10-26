using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : EnemyGlobalScript
{
    public static EnemyShooter instance;
    FSM<ShooterStates> _stateMachine;
    PatrolState _patrol;
    AttackState _attack;
    public Transform[] waypointsShooter;
    public int speed;
    public float minDistAttack;
    public int shootCooldown;
    public Player player;
    public int speedRotation;
    public LayerMask playerMask = 1<<9;
    // Start is called before the first frame update
   protected override void Start()
    {
        base.Start();
        _stateMachine = new FSM<ShooterStates>();
        _patrol = new PatrolState(this);
        _attack = new AttackState(this);

        _stateMachine.AddState(ShooterStates.Patrol, _patrol);
        _stateMachine.AddState(ShooterStates.Attack, _attack);

        ChangeState(ShooterStates.Patrol);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void ChangeState(ShooterStates state) => _stateMachine.ChangeState(state);


    public void Reset()
    {
        _life = _maxLife;
    }

    public static void TurnOnCallBack(EnemyShooter enemy)
    {
        enemy.Reset();
        enemy.gameObject.SetActive(true);
    }

    public static void TurnOffCallBack(EnemyShooter enemy)
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
            EnemyShooterFactory.instance.ReturnToPool(this);
        
     }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistAttack);
    }
}

public enum ShooterStates
{
    Patrol, 
    Attack
}

