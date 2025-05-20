using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyNormal : EnemyGlobalScript
{
    public static EnemyNormal instance;
    PatrolStateNormal _patrol;
    ChaseStateNormal _chase;
    FSM<NormalStates> _stateMachine;
    NormalView _view;
    public int speed;
    public float minDist;
    public Player player;
    public float minDistAttack;
    public event Action<float> OnLifeChange = delegate { };
    public LayerMask playerMask = 1 << 9;
    [SerializeField] Collider _collider;
    [SerializeField] Collider _colliderPunch;
    [SerializeField] float _punchDuration;
    public event Action OnDie;
    public Vector3 dir;
    public float separationRadius;
    public int maxForce;
    [Range(0f, 2f)] public float cohesionWeight = 1;
    [Range(0f, 2f)] public float separationWeight = 1;
    [Range(0f, 2f)] public float alignmentWeight = 1;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _stateMachine = new FSM<NormalStates>();
        _patrol = new PatrolStateNormal(this);
        _chase = new ChaseStateNormal(this);
        _view = new NormalView(this);

        _chase.OnMovement += _view.SetX;
        _chase.OnMovement += _view.SetZ;
        _chase.Attack += _view.Punch;


        OnDie += _view.Die;

        _stateMachine.AddState(NormalStates.Patrol, _patrol);
        _stateMachine.AddState(NormalStates.Chase, _chase);

        ChangeState(NormalStates.Patrol);

        GameManager.instance.enemyNormals.Add(this);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }


    public void ChangeState(NormalStates state) => _stateMachine.ChangeState(state);


    public void Reset()
    {
        life = _maxLife;
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
        if (life <= 0)
        {
            if (ManagerFirstZone.instance != null)
                ManagerFirstZone.instance.Kill();

            else if (ManagerFirstZone.instance == null && ManagerSecondZone.instance != null)
                ManagerSecondZone.instance.Kill();
            
            StartCoroutine(DieCoroutine());

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, minDist);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minDistAttack);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, separationRadius);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
            minDist = 50;
    }


    public IEnumerator DieCoroutine()
    {
        WaitForSeconds dieAnimation = new WaitForSeconds(dieAnimationDuration);

        var lifePotion = UnityEngine.Random.Range(1, 3);
        
        OnDie();
        //AudioManager.instance.Play(AudioManager.Sounds.DieEnemies);

        yield return dieAnimation;

        _collider.enabled = false;

        if (lifePotion == 2)
        {
            var potion = LifePotionFactory.instance.GetObjFromPool();
            potion.transform.position = transform.position;
            //AudioManager.instance.Play(AudioManager.Sounds.InstancePowerUp);
        }

        EnemyFactory.instance.ReturnToPool(this);
    }



   

}

public enum NormalStates
{
    Patrol, 
    Chase
}
