using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceball : Bullet
{
    //public GameObject Icewall;
    [SerializeField] MeshRenderer iceballMesh;
    [SerializeField] GameObject iceball1;
    [SerializeField] GameObject iceballEffects;
    [SerializeField] GameObject iceball2;
    [SerializeField] Bullet _IcebulletPrefab;

    // Start is called before the first frame update


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.layer == 6 || other.gameObject.layer == 11 || other.gameObject.layer == 15 || other.gameObject.layer == 21 || other.gameObject.layer == 20 || other.gameObject.layer == 22)
        {
            
            //Instantiate(Icewall, transform.position, Quaternion.identity);

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
            BulletFactory.instance.ReturnToPool(elementBullet, this);

        }

    }

    public override void SetChargeValues(int damage, int phase)
    {
        base.SetChargeValues(damage, phase);

        if (phase == 0)
        {
            iceballMesh.enabled = false;
            iceballEffects.SetActive(false);
            iceball1.SetActive(true);
            iceball2.SetActive(false);
        }
        else if (phase == 1)
        {
            iceballMesh.enabled = false;
            iceballEffects.SetActive(false);
            iceball1.SetActive(false);
            iceball2.SetActive(true);

        }
        else if (phase == 2)
        {
            iceballMesh.enabled = true;
            iceballEffects.SetActive(true);
            iceball2.SetActive(false);
            iceball1.SetActive(false);
        }
    }

}



