using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBossFactory : MonoBehaviour
{
    public static BulletBossFactory instance { get; private set; }
    Pool<BulletEnemy> _bulletPool;
    [SerializeField] BulletEnemy _bulletEnemyPrefab;
    [SerializeField] int _initialAmount;

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

        _bulletPool = new Pool<BulletEnemy>(CreatorMethod, BulletEnemy.TurnOnCallBack, BulletEnemy.TurnOffCallBack, _initialAmount);
    }

    BulletEnemy CreatorMethod()
    {
        return Instantiate(_bulletEnemyPrefab);
    }

    public BulletEnemy GetObjFromPool()
    {
        return _bulletPool.GetObj();
    }

    public void ReturnToPool(BulletEnemy obj)
    {
        _bulletPool.Return(obj);
    }
}
