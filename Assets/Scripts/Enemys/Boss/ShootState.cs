using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootState : State
{
    int _shootCoolDown;
    Transform _transform;
    float _viewRadius;
    float _shootTimer;
    Boss _boss;
    Transform _pivotShoot;
    int _twisterCooldown;
    List<Vector3> _warnings = new List<Vector3>();
    int _nextTwistersCooldown;
    int _startCircleAttack;
    Transform _spawnPointCircleAttack;
    public Player player;
    Vector3 _playerPos;
    Vector3 _shootPivot;
    public event Action OnShoot = delegate { };
    public event Action OnCircle = delegate { };
    public event Action OnTwister = delegate { };
    
    public ShootState(Boss boss)
    {
        _boss = boss;
        _shootCoolDown = boss.coolDownShoot;
        _transform = boss.transform;
        _viewRadius = boss.viewRadius;
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

        _playerPos.y = 0;

         _shootPivot = player.transform.position - _pivotShoot.position;

        Quaternion targetRotation = Quaternion.LookRotation(_playerPos, Vector3.up);

        

        _transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

        

        
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
        bullet.dir = _shootPivot.normalized;
        AudioManager.instance.Play(AudioManager.Sounds.BossShoot3);
        OnShoot();
    }

    IEnumerator TwisterAttack()
    {
        yield return new WaitUntil(() => _boss.enabled);


        WaitForSeconds waitForSeconds = new WaitForSeconds(_twisterCooldown);

        WaitForSeconds nextAttack = new WaitForSeconds(_nextTwistersCooldown);


        
        while (true)
        {
            yield return new WaitUntil(() => _boss.enabled);

            for (int i = 0; i < _boss.twistersAmount; i++)
            {
                yield return new WaitUntil(() => _boss.enabled);
                var spawnPointTwister = _boss.spawnPointsTwister[UnityEngine.Random.Range(0, _boss.spawnPointsTwister.Length)];

                TwisterWarning warning = TwisterWarningFactory.instance.GetObjFromPool();
                warning.transform.position = spawnPointTwister.position;
                _warnings.Add(spawnPointTwister.position);

   
            }

            yield return waitForSeconds;

            OnTwister();

            foreach (var item in _warnings)
            {
                yield return new WaitUntil(() => _boss.enabled);

                TwisterAttack attack = TwisterAttackFactory.instance.GetObjFromPool();
                attack.transform.position = item;
            }

            if(player!=null) AudioManager.instance.Play(AudioManager.Sounds.Twisters);



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
        yield return new WaitUntil(() => _boss.enabled);

        while (true)
        {
            yield return new WaitUntil(() => _boss.enabled);

            WaitForSeconds waitForSeconds = new WaitForSeconds(_boss.coolDownCircle);

            CircleAttack circleAttack = CircleAttackFactory.instance.GetObjFromPool();

            circleAttack.transform.position = _spawnPointCircleAttack.position;

            yield return waitForSeconds;

            OnCircle();

        }
    }
}
