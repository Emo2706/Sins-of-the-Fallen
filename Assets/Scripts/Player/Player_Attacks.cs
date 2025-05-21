using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attacks
{
    float _shootTimer;
    float _shootCooldown = 5;
    int _amountPowerUpBullets;
    int _powerUpBulletsCount =0;
    Bullet _bullet;
    int _multiplierDmg;
    bool _powerUpActive = false;
    Transform _pivotShoot;
    float _chargeTimer;
    float[] _phasesCooldowns;
    int _actualPhase;
    int[] _damages;
    Player _player;
    SliderUI _sliderUI;
    Player_UI _playerUI;
    bulletType currentType = bulletType.Fireball;

    public Player_Attacks(int amountPowerUpBullets , int multiplierDmg , Transform pivotShoot , Player player , SliderUI SliderUI, Player_UI playerUI)
    {
        _player = player;
        
        _amountPowerUpBullets = amountPowerUpBullets;
        _multiplierDmg = multiplierDmg;
        _pivotShoot = pivotShoot;
        _damages = player.phaseDamages;
        _phasesCooldowns = player.phasesCooldowns;
        _sliderUI = SliderUI;
        _playerUI = playerUI;
    }

    public void Update()
    {
        //_shootTimer += Time.deltaTime;

       
    }

    public void ChangeBullet()
    {
        if(currentType == bulletType.Fireball)
        {
            _playerUI.DeactivateUI(currentType);
            currentType = bulletType.Iceball;
            _playerUI.ActivateUI(currentType);
            return;
        }

        else
        {
            _playerUI.DeactivateUI(currentType);
            currentType = bulletType.Fireball;
            _playerUI.ActivateUI(currentType);
            return;
        }

    }


    public void Shoot()
    {
       

        if (_chargeTimer >= _phasesCooldowns[0])
        {

            _actualPhase = 0;
            

            if (_chargeTimer >= _phasesCooldowns[1])
            {

                _actualPhase = 1;


                if (_chargeTimer >= _phasesCooldowns[2])
                {
                    _actualPhase = 2;

                    /*_shootTimer += Time.deltaTime;
                    if(_shootTimer >= _shootCooldown)
                    {
                        Debug.Log("Cancelate LRPM");
                        ChargeUp();
                    }*/
                }
            }


        }

        _bullet = BulletFactory.instance.GetObjFromPool(currentType);
        _bullet.SetChargeValues(_damages[_actualPhase], _actualPhase);
        _bullet.transform.position = _pivotShoot.transform.position;
        _bullet.dir = Camera.main.transform.forward;

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

    public void PowerUpBullet()
    {
        _powerUpActive = true;
    }

    public void ChargeBullet()
    {
        _chargeTimer += Time.deltaTime;

        _sliderUI.FillCharge();

        if (_chargeTimer >= _shootCooldown)
        {
            Debug.Log("Cancelate LRPM");
            ChargeUp();
        }


    }

    public void ChargeUp()
    {
        Shoot();

        _chargeTimer = 0;

        _sliderUI.ResetSlidedrState();


        //AudioManager.instance.PlayRandom(new int[] { AudioManager.Sounds.Fire1, AudioManager.Sounds.Fire2, AudioManager.Sounds.Fire3, AudioManager.Sounds.Fire4 });
    }

}

