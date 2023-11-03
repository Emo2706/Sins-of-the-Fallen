using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFactory : MonoBehaviour
{
    public static BossFactory instance;
    Pool<Boss> _bossPool;
    public int initialAmount;
    [SerializeField] Boss _bossPrefab;
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

        _bossPool = new Pool<Boss>(CreathorMethod, Boss.TurnOnCallBack, Boss.TurnOffCallBack, initialAmount);
    }

    Boss CreathorMethod()
    {
        return Instantiate(_bossPrefab);
    }
    public Boss GetObjFromPool()
    {
        return _bossPool.GetObj();
    }

    public void ReturnToPool(Boss boss)
    {
        _bossPool.Return(boss);
    }
}
