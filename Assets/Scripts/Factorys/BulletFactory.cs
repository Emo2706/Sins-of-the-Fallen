using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    public static BulletFactory instance { get; private set; }
    Pool<Bullet> _fireBulletPool;
    Pool<Bullet> _iceBulletPool;
    [SerializeField] Bullet[] _bulletPrefabs;
    [SerializeField] int _initialAmount;

    public Dictionary<bulletType, Pool<Bullet>> _bulletPools = new Dictionary<bulletType, Pool<Bullet>>();

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        else
        {
            instance = this;
        }

        _fireBulletPool = new Pool<Bullet>(FireCreatorMethod, Bullet.TurnOnCallBack, Bullet.TurnOffCallBack, _initialAmount);
        _iceBulletPool = new Pool<Bullet>(IceCreatorMethod, Bullet.TurnOnCallBack, Bullet.TurnOffCallBack, _initialAmount);

        _bulletPools.Add(bulletType.Fireball, _fireBulletPool);
        _bulletPools.Add(bulletType.Iceball, _iceBulletPool);
    }

    Bullet FireCreatorMethod()
    {
 
         return Instantiate(_bulletPrefabs[0]);
    
    }

    Bullet IceCreatorMethod()
    {
         return Instantiate(_bulletPrefabs[1]);
      
    }

    public Bullet GetObjFromPool(bulletType type)
    {
        return _bulletPools[type].GetObj();
    }

    public void ReturnToPool(bulletType type, Bullet obj)
    {
        _bulletPools[type].Return(obj);
    }
}
