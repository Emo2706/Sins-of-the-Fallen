using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Factory : MonoBehaviour
{
    public static Boss2Factory instance;
    Pool<Boss2> _bossPool;
    public int initialAmount;
    [SerializeField] Boss2 _bossPrefab;
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

        _bossPool = new Pool<Boss2>(CreathorMethod, Boss2.TurnOnCallBack, Boss2.TurnOffCallBack, initialAmount);
    }

    Boss2 CreathorMethod()
    {
        return Instantiate(_bossPrefab);
    }
    public Boss2 GetObjFromPool()
    {
        return _bossPool.GetObj();
    }

    public void ReturnToPool(Boss2 boss)
    {
        _bossPool.Return(boss);
    }
}
