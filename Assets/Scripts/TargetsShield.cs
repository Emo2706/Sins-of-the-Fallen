using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsShield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            var particles = ParticleFactory.instance.GetParticleFromPool(ParticleFactory.Particle_ID.TargetHit);
            particles.transform.position = transform.position;
            AudioManager.instance.Play(AudioManager.Sounds.HitTarget);
            TargetsShieldFactory.instance.ReturnToPool(this);
            TargetsShieldFactory.instance.initialAmount--;

            if(TargetsShieldFactory.instance.initialAmount <=0) AudioManager.instance.Play(AudioManager.Sounds.ShieldBreak);
        }
    }

    private void Reset()
    {
        
    }

    public static void TurnOffCallBack(TargetsShield targetShield)
    {
        targetShield.Reset();
        targetShield.gameObject.SetActive(false);
    }


    public static void TurnOnCallBack(TargetsShield targetShield)
    {
        targetShield.gameObject.SetActive(true);
    }

}
