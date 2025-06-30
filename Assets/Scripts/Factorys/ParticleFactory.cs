using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFactory : MonoBehaviour
{
    public enum Particle_ID
    {
        ShootHit,
        TargetHit,
        Haiser,
        IceShootHit,
        ChargeFire1,
        ChargeFire2,
        ChargeFire3,
        ChargeIce1,
        ChargeIce2,
        ChargeIce3
    }
    public static ParticleFactory instance;
    public ParticleType[] particlesPrefabs;

    public Dictionary<Particle_ID, Pool<ParticleType>> _particlePools = new();

    int shootHitInitialAmount = 10;

    int targetHitInitialAmount = 5;

    int haiserHitInitialAmount = 1;

    int iceShootHitInitialAmount = 10;

    Pool<ParticleType> _shootHitPool;
    Pool<ParticleType> _targetHitPool;
    Pool<ParticleType> _haiserHitPool;
    Pool<ParticleType> _iceHitPool;
    Pool<ParticleType> _chargeFire1Pool;
    Pool<ParticleType> _chargeFire2Pool;
    Pool<ParticleType> _chargeFire3Pool;
    Pool<ParticleType> _chargeIce1Pool;
    Pool<ParticleType> _chargeIce2Pool;
    Pool<ParticleType> _chargeIce3Pool;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        _shootHitPool = new Pool<ParticleType>(ShootHitCreatorMethod, ParticleType.ParticleTurnOn, ParticleType.ParticleTurnOff, shootHitInitialAmount);
        _targetHitPool = new Pool<ParticleType>(TargetHitCreatorMethod, ParticleType.ParticleTurnOn, ParticleType.ParticleTurnOff, targetHitInitialAmount);
        _haiserHitPool = new Pool<ParticleType>(HaiserCreatorMethod, ParticleType.ParticleTurnOn, ParticleType.ParticleTurnOff, haiserHitInitialAmount);
        _iceHitPool = new Pool<ParticleType>(IceShootHitCreatorMethod, ParticleType.ParticleTurnOn, ParticleType.ParticleTurnOff, iceShootHitInitialAmount);
        _chargeFire1Pool = new Pool<ParticleType>(ChargeFire1CreatorMethod, ParticleType.ParticleTurnOn, ParticleType.ParticleTurnOff, iceShootHitInitialAmount);
        _chargeFire2Pool = new Pool<ParticleType>(ChargeFire2CreatorMethod, ParticleType.ParticleTurnOn, ParticleType.ParticleTurnOff, iceShootHitInitialAmount);
        _chargeFire3Pool = new Pool<ParticleType>(ChargeFire3CreatorMethod, ParticleType.ParticleTurnOn, ParticleType.ParticleTurnOff, iceShootHitInitialAmount);
        _chargeIce1Pool = new Pool<ParticleType>(ChargeIce1CreatorMethod, ParticleType.ParticleTurnOn, ParticleType.ParticleTurnOff, iceShootHitInitialAmount);
        _chargeIce2Pool = new Pool<ParticleType>(ChargeIce2CreatorMethod, ParticleType.ParticleTurnOn, ParticleType.ParticleTurnOff, iceShootHitInitialAmount);
        _chargeIce3Pool = new Pool<ParticleType>(ChargeIce3CreatorMethod, ParticleType.ParticleTurnOn, ParticleType.ParticleTurnOff, iceShootHitInitialAmount);

        _particlePools.Add(Particle_ID.ShootHit, _shootHitPool);
        _particlePools.Add(Particle_ID.TargetHit, _targetHitPool);
        _particlePools.Add(Particle_ID.Haiser, _haiserHitPool) ;
        _particlePools.Add(Particle_ID.IceShootHit, _iceHitPool);
        _particlePools.Add(Particle_ID.ChargeFire1, _chargeFire1Pool);
        _particlePools.Add(Particle_ID.ChargeFire2, _chargeFire2Pool);
        _particlePools.Add(Particle_ID.ChargeFire3, _chargeFire3Pool);
        _particlePools.Add(Particle_ID.ChargeIce1, _chargeIce1Pool);
        _particlePools.Add(Particle_ID.ChargeIce2, _chargeIce2Pool);
        _particlePools.Add(Particle_ID.ChargeIce3, _chargeIce3Pool);
    }
    
    ParticleType ShootHitCreatorMethod()
    {
        return Instantiate(particlesPrefabs[0]);
    }

    ParticleType TargetHitCreatorMethod()
    {
        return Instantiate(particlesPrefabs[1]);
    }

    ParticleType HaiserCreatorMethod()
    {
        return Instantiate(particlesPrefabs[2]);
    }

    ParticleType IceShootHitCreatorMethod()
    {
        return Instantiate(particlesPrefabs[3]);
    }

    ParticleType ChargeFire1CreatorMethod()
    {
        return Instantiate(particlesPrefabs[4]);
    }

    ParticleType ChargeFire2CreatorMethod()
    {
        return Instantiate(particlesPrefabs[5]);
    }

    ParticleType ChargeFire3CreatorMethod()
    {
        return Instantiate(particlesPrefabs[6]);
    }

    ParticleType ChargeIce1CreatorMethod()
    {
        return Instantiate(particlesPrefabs[7]);
    }

    ParticleType ChargeIce2CreatorMethod()
    {
        return Instantiate(particlesPrefabs[8]);
    }

    ParticleType ChargeIce3CreatorMethod()
    {
        return Instantiate(particlesPrefabs[9]);
    }


    public ParticleType GetParticleFromPool(Particle_ID pToChoose)
    {
        if (_particlePools.ContainsKey(pToChoose))
        {
            return _particlePools[pToChoose].GetObj();
        }

        return default;
    }

    public void ReturnParticleToPool(Particle_ID pToGet, ParticleType obj)
    {
        if (_particlePools.ContainsKey(pToGet))
        {
            _particlePools[pToGet].Return(obj);
        }
    }
}
