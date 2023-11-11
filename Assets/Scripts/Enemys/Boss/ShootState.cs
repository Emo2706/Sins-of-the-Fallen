using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : State
{
    int _shootCoolDown;
    Transform _transform;
    float _viewRadius;
    float _shootTimer;
    Boss _boss;
    int _circleCooldown;
    Transform _pivotShoot;
    int _twisterCooldown;
    List<Vector3> _warnings = new List<Vector3>();
    int _nextTwistersCooldown;
    int _startCircleAttack;
    Transform _spawnPointCircleAttack;
    public Player player;
    Vector3 _playerPos;
    public ShootState(Boss boss)
    {
        _boss = boss;
        _shootCoolDown = boss.coolDownShoot;
        _transform = boss.transform;
        _viewRadius = boss.viewRadius;
        _circleCooldown = boss.coolDownCircle;
        _pivotShoot = boss.pivotShoot;
        _twisterCooldown = boss.twisterCooldown;

        _nextTwistersCooldown = boss.nextTwistersCooldown;
        _startCircleAttack = boss.startCircleAttack;
        _spawnPointCircleAttack = boss.spawnPointCircle;
    }
   
    public override void OnEnter()
    {
        Debug.Log("Enter shoot");
        _shootTimer = 0;
        _boss.StartCoroutine(TwisterAttack());
        _boss.StartCoroutine(StartCircleAttack());

    }

    public override void OnExit()
    {

        _boss.StopAllCoroutines();


        Debug.Log("Exit shoot");
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnUpdate()
    {
       
        _shootTimer += Time.deltaTime;

        _playerPos = player.transform.position - _transform.position;

        _transform.forward = _playerPos;
        if (_playerPos.sqrMagnitude <= _viewRadius * _viewRadius)
        {
            if (_shootTimer >= _shootCoolDown)
            {
                Shoot();

                _shootTimer = 0;
            }

        }

 
    }

    void Shoot()
    {
        var bullet = BulletBossFactory.instance.GetObjFromPool();
        bullet.transform.position = _pivotShoot.position;
        bullet.dir = _transform.forward;
    }

    IEnumerator TwisterAttack()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_twisterCooldown);

        WaitForSeconds nextAttack = new WaitForSeconds(_nextTwistersCooldown);


        
        while (true)
        {

            for (int i = 0; i < _boss.twistersAmount; i++)
            {
                var spawnPointTwister = _boss.spawnPointsTwister[Random.Range(0, _boss.spawnPointsTwister.Length)];

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

            AudioManager.instance.Play(AudioManager.Sounds.Twisters);
           
            _warnings.RemoveRange(0 , _warnings.Count);

            

            yield return nextAttack;

        }


      

    }

    IEnumerator StartCircleAttack()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_startCircleAttack);

        yield return waitForSeconds;

        _boss.StartCoroutine(CircleAttack());
    }


    IEnumerator CircleAttack()
    {

        while (true)
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(_circleCooldown);

            CircleAttack circleAttack = CircleAttackFactory.instance.GetObjFromPool();

            circleAttack.transform.position = _spawnPointCircleAttack.position;

            yield return waitForSeconds;

        }
    }
}
