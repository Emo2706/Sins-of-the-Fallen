using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsShieldFactory : MonoBehaviour
{
    public static TargetsShieldFactory instance { get; private set; }
    Pool<TargetsShield> _targetsShieldPool;
    [SerializeField] TargetsShield _targetsShieldPrefab;
    public int initialAmount;

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

        _targetsShieldPool = new Pool<TargetsShield>(CreatorMethod, TargetsShield.TurnOnCallBack, TargetsShield.TurnOffCallBack, initialAmount);
    }

    TargetsShield CreatorMethod()
    {
        return Instantiate(_targetsShieldPrefab);
    }

    public TargetsShield GetObjFromPool()
    {
        return _targetsShieldPool.GetObj();
    }

    public void ReturnToPool(TargetsShield obj)
    {
        _targetsShieldPool.Return(obj);
    }

    
}
