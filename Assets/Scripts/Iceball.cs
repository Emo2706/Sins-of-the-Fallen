using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : Bullet
{
    public GameObject Icewall;
    [SerializeField] Bullet _IcebulletPrefab;

    // Start is called before the first frame update


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.layer == 6 || other.gameObject.layer == 11 || other.gameObject.layer == 15 || other.gameObject.layer == 21 || other.gameObject.layer == 20 || other.gameObject.layer == 22)
        {
            
            Instantiate(Icewall, transform.position, Quaternion.identity);

        }

        var haiser = other.GetComponent<GlideHaiser>();

        if (haiser != null)
        {
            var particles = ParticleFactory.instance.GetParticleFromPool(ParticleFactory.Particle_ID.ShootHit);
            particles.transform.position = transform.position;
            BulletFactory.instance.ReturnToPool(elementBullet, this);

        }

    }

}



