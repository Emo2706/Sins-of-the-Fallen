using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    float _lifeTime;
    [SerializeField] int _lifeCooldown;
    [SerializeField] int _bulletSpeed;
    public Vector3 dir;
    

   

    // Update is called once per frame
    void Update()
    {
        _lifeTime += Time.deltaTime;

        if (_lifeTime >= _lifeCooldown)
        {
            BulletEnemyFactory.instance.ReturnToPool(this);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.position += dir * _bulletSpeed * Time.deltaTime;
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 || other.gameObject.layer==11 || other.gameObject.layer== 15 || other.gameObject.layer == 9)
        {
            BulletEnemyFactory.instance.ReturnToPool(this);
        }
        
    }

    public void Reset()
    {
        _lifeTime = 0;
    }

    public static void TurnOnCallBack(BulletEnemy bullet)
    {
        bullet.Reset();
        bullet.gameObject.SetActive(true);
    }
    public static void TurnOffCallBack(BulletEnemy bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}
