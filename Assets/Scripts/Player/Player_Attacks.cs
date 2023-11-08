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
    public Player_Attacks(float shootCooldown , int amountPowerUpBullets , int multiplierDmg , Transform pivotShoot)
    {
        _shootCooldown = shootCooldown;
        _amountPowerUpBullets = amountPowerUpBullets;
        _multiplierDmg = multiplierDmg;
        _pivotShoot = pivotShoot;
    }

    public void Update()
    {
        _shootTimer += Time.deltaTime;
    }


    public void Shoot()
    {
        if (_shootTimer >= _shootCooldown)
        {
            _bullet = BulletFactory.instance.GetObjFromPool();
            _bullet.transform.position = _pivotShoot.transform.position;
            _bullet.dir = Camera.main.transform.forward;

            if (_powerUpActive == true)
            {
                _bullet.dmg *= _multiplierDmg;
                _powerUpBulletsCount++;

                if (_powerUpBulletsCount > _amountPowerUpBullets)
                    _powerUpActive = false;
            }

            else
                _bullet.dmg = 3;
           
            _shootTimer = 0;

            Debug.Log($"{_bullet.dmg}");
            Debug.Log($"{_powerUpBulletsCount}");


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

}

