using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : State
{
    int _shootCoolDown;
    public Player player;
    Transform _transform;
    float _viewRadius;
    float _shootTimer;
    int _changeStateCooldown;
    float _changeStateTimer;
    Boss _boss;
    int _circleCooldown;
    Transform _pivotShoot;
    int _twisterCooldown;
    int _twistersAmount;
    Transform[] _twistersWarningPoints;
    List<Vector3> _warnings = new List<Vector3>();
    int _nextTwistersCooldown;
    public ShootState(Boss boss)
    {
        _boss = boss;
        _shootCoolDown = boss.coolDownShoot;
        _transform = boss.transform;
        _viewRadius = boss.viewRadius;
        _changeStateCooldown = boss.coolDownChangeAttacks;
        _circleCooldown = boss.coolDownCircle;
        _pivotShoot = boss.pivotShoot;
        _twisterCooldown = boss.twisterCooldown;
        _twistersAmount = boss.twistersAmount;
        _twistersWarningPoints = boss.spawnPointsTwister;
        _nextTwistersCooldown = boss.nextTwistersCooldown;
    }
   
    public override void OnEnter()
    {
        Debug.Log("Enter shoot");
        _changeStateTimer = 0;
        _shootTimer = 0;
        _boss.StartCoroutine(TwisterAttack());
    }

    public override void OnExit()
    {
        _boss.StopCoroutine(TwisterAttack());

        Debug.Log("Exit shoot");
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
       

        _shootTimer += Time.deltaTime;

        var dir = player.transform.position - _transform.position;

        _transform.forward += dir;
        if (dir.sqrMagnitude <= _viewRadius * _viewRadius)
        {
            if (_shootTimer >= _shootCoolDown)
            {
                Shoot();

                _shootTimer = 0;
            }

        }

        _changeStateTimer += Time.deltaTime;

        if (_changeStateTimer >= _changeStateCooldown)
        {
            var changeInt = Random.Range(1, 3);

            if (changeInt == 1)
                _boss.ChangeState(BossStates.Shoot);

            if (changeInt == 2)
                _boss.ChangeState(BossStates.Zones);

        }



    }

    void Shoot()
    {
        var bullet = BulletBossFactory.instance.GetObjFromPool();
        bullet.transform.position = _pivotShoot.position;
        bullet.transform.forward = _transform.forward;
    }

    IEnumerator TwisterAttack()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_twisterCooldown);

        WaitForSeconds nextAttack = new WaitForSeconds(_nextTwistersCooldown);
        
        while (true)
        {

            for (int i = 0; i < _twistersAmount; i++)
            {
                var spawnPointTwister = _twistersWarningPoints[Random.Range(0, _twistersWarningPoints.Length)];

                TwisterWarning warning = TwisterWarningFactory.instance.GetObjFromPool();
                warning.transform.position = spawnPointTwister.position;
                _warnings.Add(spawnPointTwister.position);

   
            }

            yield return waitForSeconds;

            foreach (var item in _warnings)
            {
                TwisterAttack attack = TwisterAttackFactory.instance.GetObjFromPool();
                attack.transform.position = item;
            }

            for (int i = 0; i < _warnings.Count; i++)
            {
                 _warnings.RemoveAt(i);

            }

            yield return nextAttack;

        }


      

    }
}
