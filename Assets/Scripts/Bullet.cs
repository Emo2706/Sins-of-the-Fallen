using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float _lifeTime;
    [SerializeField] int _lifeCooldown;
    [SerializeField] int _bulletSpeed;
    public Vector3 dir;
    Rigidbody _rb;
    [SerializeField] ParticleSystem _explosion;
    public int dmg ;
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
            BulletFactory.instance.ReturnToPool(this);
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.layer == 11 || other.gameObject.layer==15 || other.gameObject.layer==21 || other.gameObject.layer == 20 || other.gameObject.layer == 22)
        {
            var particles =   ParticleFactory.instance.GetParticleFromPool(ParticleFactory.Particle_ID.ShootHit);
            particles.transform.position = transform.position;
            BulletFactory.instance.ReturnToPool(this);
            
        }

        var enemy = other.GetComponent<EnemyGlobalScript>();

        if (enemy != null)
        {
            enemy.TakeDmg(dmg);
            AudioManager.instance.Play(AudioManager.Sounds.DmgEnemies);
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
