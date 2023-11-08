using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePotionFactory : MonoBehaviour
{
    public static LifePotionFactory instance { get; private set; }
    Pool<LifePotion> _lifePotionPool;
    [SerializeField] LifePotion _lifePotionPrefab;
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

        _lifePotionPool = new Pool<LifePotion>(CreatorMethod, LifePotion.TurnOnCallBack, LifePotion.TurnOffCallBack, _initialAmount);
    }

    LifePotion CreatorMethod()
    {
        return Instantiate(_lifePotionPrefab);
    }

    public LifePotion GetObjFromPool()
    {
        return _lifePotionPool.GetObj();
    }

    public void ReturnToPool(LifePotion obj)
    {
        _lifePotionPool.Return(obj);
    }

}
