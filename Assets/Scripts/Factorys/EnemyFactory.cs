using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory instance { get; private set; }
    Pool<EnemyNormal> _enemyPool;
    [SerializeField] EnemyNormal _enemy;
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

        _enemyPool = new Pool<EnemyNormal>(CreatorMethod , EnemyNormal.TurnOnCallBack, EnemyNormal.TurnOffCallBack, _initialAmount);
    }
    

    

    EnemyNormal CreatorMethod()
    {
        return Instantiate(_enemy);
    }

    public EnemyNormal GetObjFromPool()
    {
       return _enemyPool.GetObj();
    }

    public void ReturnToPool(EnemyNormal enemy)
    {
        _enemyPool.Return(enemy);
    }

   
}
