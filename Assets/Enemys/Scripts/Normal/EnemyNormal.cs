using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyNormal : Entity
{
    public static EnemyNormal instance;
    PatrolStateNormal _patrol;
    ChaseStateNormal _chase;
    FiniteStateMachineNormal _stateMachine;
    public Rigidbody rb;
    public int speed;
    public float minDist;
    public Player player;
    public float minDistAttack;
    public event Action<float> OnMovement = delegate { };
    public event Action<float> OnLifeChange = delegate { };
    public event Action OnJump = delegate { };
    public event Action OnDead = delegate { };
    public LayerMask playerMask = 1 << 9;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _stateMachine = new FiniteStateMachineNormal();
        _patrol = new PatrolStateNormal(this);
        _chase = new ChaseStateNormal(this);

        _stateMachine.AddState(NormalStates.Patrol, _patrol);
        _stateMachine.AddState(NormalStates.Chase, _chase);

        _stateMachine.ChangeState(NormalStates.Patrol);
      
    }

   

    private void FixedUpdate()
    {
        _stateMachine.ArtificialUpdate();
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
        Gizmos.DrawWireSphere(transform.position, minDist);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistAttack);
    }

    
}

public enum NormalStates
{
    Patrol, 
    Chase
}
