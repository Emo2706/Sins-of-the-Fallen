using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideHaiser : MonoBehaviour
{
    public bool activate;
    public ParticleSystem preHaiser;
    [SerializeField] AudioSource _start;
    [SerializeField] AudioSource _loop;

    public void GetHaiser()
    {
        activate = true;
        var particles = ParticleFactory.instance.GetParticleFromPool(ParticleFactory.Particle_ID.Haiser);
        particles.transform.position = transform.position;
        preHaiser.Stop();
        StartCoroutine(HaiserSounds());
    }

    IEnumerator HaiserSounds()
    {
        _start.Play();

        yield return new WaitForSeconds(0.5f);

        _loop.Play();
    }
}
