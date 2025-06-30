using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attacks
{
    float _shootTimer;
    float _shootCooldown = 3;
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
    PlayerView _view;
    ParticleType _particles;

    bool _blended1 , _blended2, _blended3;

    public Player_Attacks(int amountPowerUpBullets , int multiplierDmg , Transform pivotShoot , Player player , SliderUI SliderUI, Player_UI playerUI, PlayerView view)
    {
        _player = player;
        
        _amountPowerUpBullets = amountPowerUpBullets;
        _multiplierDmg = multiplierDmg;
        _pivotShoot = pivotShoot;
        _damages = player.phaseDamages;
        _phasesCooldowns = player.phasesCooldowns;
        _sliderUI = SliderUI;
        _playerUI = playerUI;
        _view = view;
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
    }

    public void PowerUpBullet()
    {
        _powerUpActive = true;
    }

    public void ChargeBullet()
    {
        _chargeTimer += Time.deltaTime;

        if(_chargeTimer >= _phasesCooldowns[0] && !_blended1)
        {
            _view.BlendAnimations(0.33f);

            if(currentType== bulletType.Fireball)
                //_particles = ParticleFactory.instance.GetParticleFromPool(ParticleFactory.Particle_ID.ChargeFire1);

            // else _particles = ParticleFactory.instance.GetParticleFromPool(ParticleFactory.Particle_ID.ChargeIce1);

            _blended1 = true;
        } 

        if(_chargeTimer >= _phasesCooldowns[1] && !_blended2)
        {
            _view.BlendAnimations(0.66f);

            //ParticleFactory.instance.ReturnParticleToPool(_particles.TypeOfParticle, _particles)

            if (currentType == bulletType.Fireball)
                //_particles = ParticleFactory.instance.GetParticleFromPool(ParticleFactory.Particle_ID.ChargeFire2);

            // else _particles = ParticleFactory.instance.GetParticleFromPool(ParticleFactory.Particle_ID.ChargeIce2);
            _blended2 = true;
        } 
        
        if(_chargeTimer >= _phasesCooldowns[2] && !_blended3)
        {
            //ParticleFactory.instance.ReturnParticleToPool(_particles.TypeOfParticle, _particles)
            _view.BlendAnimations(1f);

            if(currentType == bulletType.Fireball)
                //_particles = ParticleFactory.instance.GetParticleFromPool(ParticleFactory.Particle_ID.ChargeFire3);

            //else ParticleFactory.instance.GetParticleFromPool(ParticleFactory.Particle_ID.ChargeIce3);
                
            _blended3 = true;
        }

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

        _view.Shoot();

        _view.BlendAnimations(0);

        _blended1 = false;
        _blended2 = false;
        _blended3 = false;

        _chargeTimer = 0;

        _sliderUI.ResetSlidedrState();

        //ParticlesFactory.instance.ReturnParticleToPool(_particles.TypeOfParticle , _particles)

        AudioManager.instance.PlayRandom(new int[] { AudioManager.Sounds.Fire1, AudioManager.Sounds.Fire2, AudioManager.Sounds.Fire3, AudioManager.Sounds.Fire4 });
    }

}

