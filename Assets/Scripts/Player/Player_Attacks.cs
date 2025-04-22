using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attacks
{
    float _shootTimer;
    float _shootCooldown;
    int _amountPowerUpBullets;
    int _powerUpBulletsCount =0;
    Bullet _bullet;
    int _multiplierDmg;
    bool _powerUpActive = false;
    Transform _pivotShoot;
    float _chargeTimer;
    float[] _phasesCooldowns;
    int _phase1Dmg;
    int _phase2Dmg;
    Player _player;
    SliderUI _sliderUI;
    public Player_Attacks(float shootCooldown , int amountPowerUpBullets , int multiplierDmg , Transform pivotShoot , Player player , SliderUI SliderUI)
    {
        _player = player;
        _shootCooldown = shootCooldown;
        _amountPowerUpBullets = amountPowerUpBullets;
        _multiplierDmg = multiplierDmg;
        _pivotShoot = pivotShoot;
        _phase1Dmg = player.phase1Dmg;
        _phase2Dmg = player.phase2Dmg;
        _phasesCooldowns = player.phasesCooldowns;
        _sliderUI = SliderUI;
    }

    public void Update()
    {
        //_shootTimer += Time.deltaTime;

       
    }


    public void Shoot()
    {
       // if (_shootTimer >= _shootCooldown)
        {
            if(_chargeTimer >= _phasesCooldowns[0])
            {
                _bullet = BulletFactory.instance.GetObjFromPool();
                _bullet.transform.position = _pivotShoot.transform.position;
                _bullet.dir = Camera.main.transform.forward;
                _bullet.dmg = 3;
                Debug.Log("1");
                

                if (_chargeTimer >= _phasesCooldowns[1])
                {
                    Debug.Log("2");

                    _bullet.dmg = _phase1Dmg;

                    
                    if (_chargeTimer >= _phasesCooldowns[2])
                    {
                        _bullet.dmg = _phase2Dmg;
                        Debug.Log("3");
                        
                    }
                }


            }

            if (_powerUpActive == true)
            {
                _bullet.dmg *= _multiplierDmg;
                _powerUpBulletsCount++;

                if (_powerUpBulletsCount > _amountPowerUpBullets)
                    _powerUpActive = false;
            }


           

            /*Debug.Log($"{_bullet.dmg}");
            Debug.Log($"{_powerUpBulletsCount}");*/


            /*if (_powerUpBulletsCount <= _amountPowerUpBullets)
                _powerUpBulletsCount++;


            if (_powerUpBulletsCount > _amountPowerUpBullets)
            {
                _bullet.dmg = 3;
                _powerUpBulletsCount = 0;
            }*/
                
        }
    }

    public void PowerUpBullet()
    {
        _powerUpActive = true;
    }

    public void ChargeBullet()
    {
        _chargeTimer += Time.deltaTime;

        _sliderUI.FillCharge();



        Debug.Log("Charge");
    }

    public void ChargeUp()
    {
        Shoot();

        _chargeTimer = 0;

        _sliderUI.ResetSlidedrState();

        Debug.Log("Shoot");

        //AudioManager.instance.PlayRandom(new int[] { AudioManager.Sounds.Fire1, AudioManager.Sounds.Fire2, AudioManager.Sounds.Fire3, AudioManager.Sounds.Fire4 });
    }

}

