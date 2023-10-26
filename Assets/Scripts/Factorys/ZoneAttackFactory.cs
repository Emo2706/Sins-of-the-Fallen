using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAttackFactory : MonoBehaviour
{
    public static ZoneAttackFactory instance;
    Pool<ZoneAttack> _zoneAttackPool;
    public int initialAmount;
    [SerializeField] ZoneAttack _zone;
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

        _zoneAttackPool = new Pool<ZoneAttack>(CreathorMethod, ZoneAttack.TurnOnCallBack, ZoneAttack.TurnOffCallBack, initialAmount);
    }

    ZoneAttack CreathorMethod()
    {
        return Instantiate(_zone);
    }
    public ZoneAttack GetObjFromPool()
    {
        return _zoneAttackPool.GetObj();
    }

    public void ReturnToPool(ZoneAttack zone)
    {
        _zoneAttackPool.Return(zone);
    }
    
}
