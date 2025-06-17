using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float _lifeTime;
    [SerializeField] int _lifeCooldown;
    public int _bulletSpeed;
    public Vector3 dir;
    Rigidbody _rb;
    [SerializeField] ParticleSystem _explosion;
    public int dmg;
    public bool chargePhase1, chargePhase2, chargePhase3;
    public bulletType elementBullet;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _lifeTime += Time.deltaTime;

        if (_lifeTime>=_lifeCooldown)
        {
            BulletFactory.instance.ReturnToPool(elementBullet, this);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        _rb.velocity= dir.normalized * _bulletSpeed * Time.deltaTime;
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.layer == 11 || other.gameObject.layer==15 || other.gameObject.layer==21 || other.gameObject.layer == 20 || other.gameObject.layer == 22)
        {
            if(elementBullet == bulletType.Fireball)
            {
                var fireParticles = ParticleFactory.instance.GetParticleFromPool(ParticleFactory.Particle_ID.ShootHit);
                fireParticles.transform.position = transform.position;
            }
            else
            {
                var iceParticles = ParticleFactory.instance.GetParticleFromPool(ParticleFactory.Particle_ID.IceShootHit);
                iceParticles.transform.position = transform.position;
            }
            
            BulletFactory.instance.ReturnToPool(elementBullet,this);
            
        }



        var enemy = other.GetComponent<EnemyGlobalScript>();

        if (enemy != null)
        {
            enemy.TakeDmg(dmg);
            //AudioManager.instance.Play(AudioManager.Sounds.DmgEnemies);// cambiar segun bala
            
        }
    }

    public virtual void SetChargeValues(int damage, int phase)
    {
        dmg = damage;
        Debug.Log(dmg);
        if(phase == 0)
        {
            chargePhase1 = true;
        }
        else if (phase == 1)
        {
            chargePhase2 = true;
        }
        else if (phase == 2)
        {
            chargePhase3 = true;
        }

    }


    public void Reset()
    {
        _lifeTime = 0;
    }

    public static void TurnOnCallBack(Bullet bullet)
    {
        bullet.Reset();
        bullet.gameObject.SetActive(true);
    }
    public static void TurnOffCallBack(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}

public enum bulletType
{
    Fireball,
    Iceball
}