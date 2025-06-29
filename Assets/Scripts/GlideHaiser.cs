using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideHaiser : MonoBehaviour
{
    public bool activate;
    public ParticleSystem preHaiser;

    public void GetHaiser()
    {
        activate = true;
        var particles = ParticleFactory.instance.GetParticleFromPool(ParticleFactory.Particle_ID.Haiser);
        particles.transform.position = transform.position;
        preHaiser.Stop();
    }
}
