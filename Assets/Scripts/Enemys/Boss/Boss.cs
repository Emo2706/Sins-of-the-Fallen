using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyGlobalScript
{
    FSM<BossStates> _stateMachine;
    ShootState _shoot;
    ZonesState _zones;

    Dictionary<BossStates, StatesId> _statesDictionary;

    public float viewRadius;
    public int coolDownChangeAttacks;
    public int coolDownCircle;
    public int coolDownShoot;
    public LayerMask playerMask = 1<<9;
    public Transform pivotShoot;
    public int zoneAttackCooldown;
    public int twisterCooldown;
    public int twistersAmount;
    public int nextTwistersCooldown;
    public Transform[] spawnPointsZone;
    public Transform[] spawnPointsTwister;
    
    protected override void Start()
    {
        base.Start();



        //_statesDictionary = new Dictionary<BossStates, StatesId>();

        _stateMachine = new FSM<BossStates>();
        _shoot = new ShootState(this);
        _zones = new ZonesState(this);


        _stateMachine.AddState(BossStates.Shoot ,_shoot);
        _stateMachine.AddState(BossStates.Zones ,_zones);


        ChangeState(BossStates.Shoot);
    }

    public void ChangeState(BossStates state) => _stateMachine.ChangeState(state);

    
    void Update()
    {
        var player = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        foreach (var item in player)
        {
            if (item.GetComponent<Player>()!=null)
            {
                _shoot.player = item.GetComponent<Player>();
                _stateMachine.Update();

            }
        }
        

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

public struct StatesId
{
    public const int shootState = 0;
    public const int zonesState = 1;
}
