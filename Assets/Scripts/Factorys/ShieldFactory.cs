using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFactory : MonoBehaviour
{
    public static ShieldFactory instance { get; private set; }
    Pool<Shield> _shieldPool;
    [SerializeField] Shield _shieldPrefab;
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

        _shieldPool = new Pool<Shield>(CreatorMethod, Shield.TurnOnCallBack, Shield.TurnOffCallBack, _initialAmount);
    }

    Shield CreatorMethod()
    {
        return Instantiate(_shieldPrefab);
    }

    public Shield GetObjFromPool()
    {
        return _shieldPool.GetObj();
    }

    public void ReturnToPool(Shield obj)
    {
        _shieldPool.Return(obj);
    }
}
