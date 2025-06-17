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
        IceShootHit
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

        _particlePools.Add(Particle_ID.ShootHit, _shootHitPool);
        _particlePools.Add(Particle_ID.TargetHit, _targetHitPool);
        _particlePools.Add(Particle_ID.Haiser, _haiserHitPool) ;
        _particlePools.Add(Particle_ID.IceShootHit, _iceHitPool);
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    
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
