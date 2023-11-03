using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyGlobalScript
{
    FSM<BossStates> _stateMachine;
    ShootState _shoot;
    ZonesState _zones;

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

    [Header("SpawnPoints")]
    public Transform pivotShoot;
    public Transform[] spawnPointsZone;
    public Transform[] spawnPointsTwister;
    public Transform spawnPointCircle;

    float _changeAttackTimer;
    BossStates[] _bossStates = new BossStates[] { BossStates.Shoot, BossStates.Zones };


    protected override void Start()
    {
        base.Start();


        
       

        _stateMachine = new FSM<BossStates>();
        _shoot = new ShootState(this);
        _zones = new ZonesState(this);


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
            _stateMachine.Update();

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


