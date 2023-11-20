using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFactory : MonoBehaviour
{
    public enum Particle_ID
    {
        ShootHit
    }
    public static ParticleFactory instance;
    public ParticleType[] particlesPrefabs;

    public Dictionary<Particle_ID, Pool<ParticleType>> _particlePools = new();

    int shootHitInitialAmount = 10;

    Pool<ParticleType> _shootHitPool;
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

        _particlePools.Add(Particle_ID.ShootHit, _shootHitPool);
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    
    ParticleType ShootHitCreatorMethod()
    {
        return Instantiate(particlesPrefabs[0]);
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
