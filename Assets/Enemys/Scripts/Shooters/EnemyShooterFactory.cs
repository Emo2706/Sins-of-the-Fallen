using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterFactory : MonoBehaviour
{
    public static EnemyShooterFactory instance { get; private set; }
    Pool<EnemyShooter> _enemyShooterPool;
    public int initialAmount;
    [SerializeField] EnemyShooter _enemyShooterPrefab;

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

        _enemyShooterPool = new Pool<EnemyShooter>(CreatorMethod, EnemyShooter.TurnOnCallBack, EnemyShooter.TurnOffCallBack, initialAmount);
    }

    EnemyShooter CreatorMethod()
    {
      return Instantiate(_enemyShooterPrefab);
    }

    public EnemyShooter GetObjFromPool()
    {
        return _enemyShooterPool.GetObj();
    }

    public void ReturnToPool(EnemyShooter enemy)
    {
        _enemyShooterPool.Return(enemy);
    }

}
