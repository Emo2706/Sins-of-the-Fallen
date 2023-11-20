using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleType : MonoBehaviour
{
    [SerializeField] protected float _livingTime;
    protected ParticleSystem _ParSys;
    public ParticleFactory.Particle_ID TypeOfParticle;

    protected virtual void Awake()
    {
        _ParSys = gameObject.GetComponent<ParticleSystem>();

    }
  

    public static void ParticleTurnOff(ParticleType p)
    {
        p.gameObject.SetActive(false);
        p._ParSys.Stop();
    }
    public static void ParticleTurnOn(ParticleType p)
    {
        p.gameObject.SetActive(true);
        p._ParSys.Play();
        p.StartCoroutine(p.CorroutineDeactivate());
        

    }

    public IEnumerator CorroutineDeactivate()
    {
        yield return new WaitForSeconds(_livingTime);
        ReturnMe();

    }

   protected virtual void ReturnMe()
    {
        ParticleFactory.instance.ReturnParticleToPool(TypeOfParticle, this);
    }
 
    protected virtual void InitializeLivingTime()
    {
        _livingTime = _ParSys.main.duration;
    }

}
