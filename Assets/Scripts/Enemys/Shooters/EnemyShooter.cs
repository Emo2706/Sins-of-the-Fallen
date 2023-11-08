using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : EnemyGlobalScript
{
    public static EnemyShooter instance;
    FSM<ShooterStates> _stateMachine;
    PatrolState _patrol;
    AttackState _attack;
    ShooterView _view;
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
        _view = new ShooterView(this);

        _attack.OnShoot += _view.Shoot;

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
        life = _maxLife;
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
        if (life <= 0)
            StartCoroutine(DieCoroutine());
        
     }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistAttack);
    }

    public IEnumerator DieCoroutine()
    {
        WaitForSeconds dieAnimation = new WaitForSeconds(dieAnimationDuration);

        var lifePotion = Random.Range(1, 3);

        yield return dieAnimation;

        if (lifePotion == 2)
        {
            var potion = LifePotionFactory.instance.GetObjFromPool();
            potion.transform.position = transform.position;
        }

        EnemyShooterFactory.instance.ReturnToPool(this);
    }
}

public enum ShooterStates
{
    Patrol, 
    Attack
}

