using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFactory : MonoBehaviour
{
    public enum Particle_ID
    {
        ShootHit,
        TargetHit
    }
    public static ParticleFactory instance;
    public ParticleType[] particlesPrefabs;

    public Dictionary<Particle_ID, Pool<ParticleType>> _particlePools = new();

    int shootHitInitialAmount = 10;

    int targetHitInitialAmount = 5;

    Pool<ParticleType> _shootHitPool;
    Pool<ParticleType> _targetHitPool;
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

        _particlePools.Add(Particle_ID.ShootHit, _shootHitPool);
        _particlePools.Add(Particle_ID.TargetHit, _targetHitPool);
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
