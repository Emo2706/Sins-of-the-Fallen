using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAttackWarningFactory : MonoBehaviour
{
    public static ZoneAttackWarningFactory instance;
    Pool<ZoneAttackWarning> _zoneAttackWarningPool;
    public int initialAmount;
    [SerializeField] ZoneAttackWarning _zoneWarning;
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

        _zoneAttackWarningPool = new Pool<ZoneAttackWarning>(CreathorMethod, ZoneAttackWarning.TurnOnCallBack, ZoneAttackWarning.TurnOffCallBack, initialAmount);
    }

    ZoneAttackWarning CreathorMethod()
    {
        return Instantiate(_zoneWarning);
    }
    public ZoneAttackWarning GetObjFromPool()
    {
        return _zoneAttackWarningPool.GetObj();
    }

    public void ReturnToPool(ZoneAttackWarning zoneWarning)
    {
        _zoneAttackWarningPool.Return(zoneWarning);
    }

    
}
