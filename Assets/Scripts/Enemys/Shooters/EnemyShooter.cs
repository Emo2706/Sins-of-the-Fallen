using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyShooter : EnemyGlobalScript
{
    public static EnemyShooter instance;
    FSM<ShooterStates> _stateMachine;
    PatrolState _patrol;
    AttackState _attack;
    ShooterView _view;
    public float minDistAttack;
    public float shootCooldown;
    public Player player;
    public int speedRotation;
    public LayerMask playerMask = 1<<9;
    public Transform pivotShoot;
    public event Action OnDie;
    // Start is called before the first frame update
   protected override void Start()
    {
        base.Start();
        _stateMachine = new FSM<ShooterStates>();
        _patrol = new PatrolState(this);
        _attack = new AttackState(this);
        _view = new ShooterView(this);

        _attack.OnShoot += _view.Shoot;
        OnDie += _view.Die;

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
        {
            if (ManagerSecondZone.instance != null)
                ManagerSecondZone.instance.Kill();

            StartCoroutine(DieCoroutine());
        }
        
     }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistAttack);
    }

    public IEnumerator DieCoroutine()
    {
        WaitForSeconds dieAnimation = new WaitForSeconds(dieAnimationDuration);

        var lifePotion = UnityEngine.Random.Range(1, 2);

        OnDie();
        AudioManager.instance.Play(AudioManager.Sounds.DieEnemies);

        yield return dieAnimation;

        if (lifePotion == 2)
        {
            var potion = LifePotionFactory.instance.GetObjFromPool();
            potion.transform.position = transform.position;
            AudioManager.instance.Play(AudioManager.Sounds.InstancePowerUp);
        }

        EnemyShooterFactory.instance.ReturnToPool(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
            minDistAttack = 30;
    }
}

public enum ShooterStates
{
    Patrol, 
    Attack
}

