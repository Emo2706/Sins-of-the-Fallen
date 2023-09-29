using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyersFactory : MonoBehaviour
{
    public static EnemyFlyersFactory instance;
    Pool<EnemyFlyers> _enemyFlyersPool;
    public int initialAmount;
    [SerializeField] EnemyFlyers _enemyFlyerPrefab;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        else
            instance = this;

        _enemyFlyersPool = new Pool<EnemyFlyers>(CreathorMethod, EnemyFlyers.TurnOnCallBack, EnemyFlyers.TurnOffCallBack, initialAmount);
    }

    EnemyFlyers CreathorMethod()
    {
        return Instantiate(_enemyFlyerPrefab);
    }
    public EnemyFlyers GetObjFromPool()
    {
        return _enemyFlyersPool.GetObj();
    }

    public void ReturnToPool(EnemyFlyers enemy)
    {
         _enemyFlyersPool.Return(enemy);
    }
}
