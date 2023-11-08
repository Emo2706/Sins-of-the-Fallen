using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyGlobalScript
{
    FSM<BossStates> _stateMachine;
    ShootState _shoot;
    ZonesState _zones;
    LifeHandlerBoss _lifeHandler;

    public float viewRadius;
    public LayerMask playerMask = 1<<9;
    
    [Header("Cooldowns")]
    public int zoneAttackCooldown;
    public int twisterCooldown;
    public int twistersAmount;
    public int nextTwistersCooldown;
    public int startCircleAttack;
    public int cooldownChangeAttackCircle;
    public int coolDownChangeAttacks;
    public int coolDownCircle;
    public int coolDownShoot;
    [SerializeField] int _coolDownPotion;

    [Header("SpawnPoints")]
    public Transform pivotShoot;
    public Transform[] spawnPointsZone;
    public Transform[] spawnPointsTwister;
    public Transform spawnPointCircle;
    [SerializeField] Transform[] _spawnPointsPotions;
    [SerializeField] Transform[] _spawnPointsTargetsShield;

    float _changeAttackTimer;
    float _lifePotionTimer;

    Shield _shield;
    BossStates[] _bossStates = new BossStates[] { BossStates.Shoot, BossStates.Zones };


    private void Awake()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_BossHalfLife , Shield); 
    }

    protected override void Start()
    {
        base.Start();


        _stateMachine = new FSM<BossStates>();
        _shoot = new ShootState(this);
        _zones = new ZonesState(this);
        _lifeHandler = new LifeHandlerBoss();

        _stateMachine.AddState(BossStates.Shoot ,_shoot);
        _stateMachine.AddState(BossStates.Zones ,_zones);


        ChangeState(BossStates.Shoot);

    }

    public void RandomChangeState()
    {
        _stateMachine.ChangeState(_bossStates[Random.Range(0, _bossStates.Length)]);
        _changeAttackTimer = 0;
    }

    public void ChangeState(BossStates state) => _stateMachine.ChangeState(state);

    
    void Update()
    {
        if (_shoot.player == null)
        {
            var playerCollider = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

            foreach (var item in playerCollider)
            {
                if (item.GetComponent<Player>()!=null)
                {
                    _shoot.player = item.GetComponent<Player>();

                }
            }

        }

        else
        {
            _stateMachine.Update();

            _lifePotionTimer += Time.deltaTime;

            if (_lifePotionTimer >= _coolDownPotion)
            {
                _lifePotionTimer = 0;

                var spawnPoint = Random.Range(0, _spawnPointsPotions.Length);

                var potion = LifePotionFactory.instance.GetObjFromPool();
                potion.transform.position = _spawnPointsPotions[spawnPoint].position;
            }


           // if (life == _maxLife / 2) _lifeHandler.HalfLife();
            


            if (TargetsShieldFactory.instance.initialAmount <= 0)
            {
                ShieldFactory.instance.ReturnToPool(_shield);
            }
        }

        _changeAttackTimer += Time.deltaTime;

        if (_changeAttackTimer >= coolDownChangeAttacks) RandomChangeState();




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
            BossFactory.instance.ReturnToPool(this);
        }

        if (life == _maxLife / 2) Shield();
    }


    void Shield(params object[] p)
    {
        _shield = ShieldFactory.instance.GetObjFromPool();
        _shield.transform.position = transform.position;

        foreach (var item in _spawnPointsTargetsShield)
        {
            var targetShield = TargetsShieldFactory.instance.GetObjFromPool();
            targetShield.transform.position = item.position;
        }

        EventManager.UnSubscribeToEvent(EventManager.EventsType.Event_BossHalfLife , Shield);
    }


    public static void TurnOnCallBack(Boss boss)
    {
        boss.gameObject.SetActive(true);
    }

    public static void TurnOffCallBack(Boss boss)
    {
        boss.gameObject.SetActive(false);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

}

public enum BossStates
{
    Shoot,
    Zones,
}


