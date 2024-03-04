using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : EnemyGlobalScript
{
    FSM<BossStates> _stateMachine;
    ShootState _shoot;
    ZonesState _zones;
    LifeHandlerBoss _lifeHandler;
    BossView _view;
  

    public float viewRadius;
    public LayerMask playerMask = 1<<9;
    
    [Header("Cooldowns")]
    public int zoneAttackCooldown;
    public int twisterCooldown;
    public int twistersAmount;
    public int twistersAmountPhase2;
    public int twistersAmountPhase3;
    public int nextTwistersCooldown;
    public int startCircleAttack;
    public int cooldownChangeAttackCircle;
    public int coolDownChangeAttacks;
    public int coolDownCircle;
    [SerializeField] int _coolDownCirclePhase2;
    [SerializeField] int _coolDownCirclePhase3;
    public int coolDownShoot;
    [SerializeField] int _coolDownPotion;

    [Header("SpawnPoints")]
    public Transform pivotShoot;
    public Transform[] spawnPointsZone;
    public Transform[] spawnPointsTwister;
    public Transform spawnPointCircle;
    public Transform[] spawnPointsPotions;
    public Transform[] spawnPointsTargetsShield;
    [SerializeField] Transform _spawnPointShield;

    float _changeAttackTimer;
    float _lifePotionTimer;
    bool _canActivateShield = true;

    [SerializeField] int _lifePhase3Cooldown;


    

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
        _view = new BossView(this);
        

        _stateMachine.AddState(BossStates.Shoot ,_shoot);
        _stateMachine.AddState(BossStates.Zones ,_zones);


        ChangeState(BossStates.Shoot);

        _zones.Zone += _view.Zone;

    }

    public void RandomChangeState()
    {
        _stateMachine.ChangeState(_bossStates[Random.Range(0, _bossStates.Length)]);
        _changeAttackTimer = 0;
    }

    public void ChangeState(BossStates state) => _stateMachine.ChangeState(state);

    
    void Update()
    {
        if (GameManager.instance.pause == true) return;


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

                var spawnPoint = Random.Range(0, spawnPointsPotions.Length);

                var potion = LifePotionFactory.instance.GetObjFromPool();
                AudioManager.instance.Play(AudioManager.Sounds.InstancePowerUp);
                potion.transform.position = spawnPointsPotions[spawnPoint].position;
            }

            


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
            _lifeHandler.Defeated();

            BossFactory.instance.ReturnToPool(this);
        }

        if (life <= _maxLife / 2)
        {
            if (_canActivateShield == true)
            {
                Shield();
                twistersAmount = twistersAmountPhase2;
                coolDownCircle = _coolDownCirclePhase2;
                AudioManager.instance.Play(AudioManager.Sounds.Shield);
                _canActivateShield = false;
            }

        }

        if (life <= _lifePhase3Cooldown)
        {
            coolDownCircle = _coolDownCirclePhase3;
            twistersAmount = twistersAmountPhase3;
        }
    }


    void Shield(params object[] p)
    {
        _shield = ShieldFactory.instance.GetObjFromPool();
        _shield.transform.position = _spawnPointShield.position;

        foreach (var item in spawnPointsTargetsShield)
        {
            var targetShield = TargetsShieldFactory.instance.GetObjFromPool();
            targetShield.transform.position = item.position;
        }

        AudioManager.instance.Play(AudioManager.Sounds.Shield);

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


