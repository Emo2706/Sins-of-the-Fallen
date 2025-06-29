using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Bullet
{
    public int burnDamage = 10;
    public float burnDuration = 5f;
    public float burnInterval = 1f;
    [SerializeField] MeshRenderer fireballMesh;
    [SerializeField] GameObject fireball1;
    [SerializeField] GameObject fireballEffects;
    [SerializeField] GameObject fireball2;

    // Start is called before the first frame update


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.layer == 6 || other.gameObject.layer == 11 || other.gameObject.layer == 15 || other.gameObject.layer == 21 || other.gameObject.layer == 20 || other.gameObject.layer == 22)
        {

            //enemy.ApplyBurn(burnDamage, burnDuration, burnInterval); //poner en clase fireball
            /*foreach (GameObject particle in enemy._buffParticles)
            {
                particle.SetActive(true);
            }*/

            //aca meter los efectos de cada una
            if (chargePhase1)
            {
                return;
            }
            if (chargePhase2)
            {
                return;
            }
            if (chargePhase3)
            {
                return;
            }

        }

        var haiser = other.GetComponent<GlideHaiser>();

        if (haiser != null)
        {
            var particles = ParticleFactory.instance.GetParticleFromPool(ParticleFactory.Particle_ID.ShootHit);
            particles.transform.position = transform.position;
            haiser.GetHaiser();
            BulletFactory.instance.ReturnToPool(elementBullet, this);

        }

    }

    public override void SetChargeValues(int damage, int phase)
    {
        base.SetChargeValues(damage, phase);

        if (phase == 0)
        {
            fireballMesh.enabled = false;
            fireballEffects.SetActive(false);
            fireball1.SetActive(true);
            fireball2.SetActive(false);
        }
        else if (phase == 1)
        {
            fireballMesh.enabled = false;
            fireballEffects.SetActive(false);
            fireball1.SetActive(false);
            fireball2.SetActive(true);

        }
        else if (phase == 2)
        {
            fireballMesh.enabled = true;
            fireballEffects.SetActive(true);
            fireball2.SetActive(false);
            fireball1.SetActive(false);
        }
    }


}



