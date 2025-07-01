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
        StartCoroutine(HaiserSounds());
    }

    IEnumerator HaiserSounds()
    {
        AudioManager.instance.Play(AudioManager.Sounds.HaiserStart);

        yield return new WaitForSeconds(0.3f);

        AudioManager.instance.Play(AudioManager.Sounds.Haiser);
    }
}
