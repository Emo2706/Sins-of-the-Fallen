using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Bullet
{
    public int burnDamage = 10;
    public float burnDuration = 5f;
    public float burnInterval = 1f;

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

}



