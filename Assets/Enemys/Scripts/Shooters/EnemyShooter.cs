using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Entity
{
    public static EnemyShooter instance;
    FiniteStateMachineShooter _stateMachine;
    PatrolState _patrol;
    AttackState _attack;
    public Transform[] waypointsShooter;
    public int speed;
    public float minDistAttack;
    public int shootCooldown;
    public Player player;
    public int speedRotation;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();//preguntar como hacerlo sin FindObject
        _stateMachine = new FiniteStateMachineShooter();
        _patrol = new PatrolState(this);
        _attack = new AttackState(this);

        _stateMachine.AddState(ShooterStates.Patrol, _patrol);
        _stateMachine.AddState(ShooterStates.Attack, _attack);

        _stateMachine.ChangeState(ShooterStates.Patrol);

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

