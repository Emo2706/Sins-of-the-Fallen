using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwisterWarningFactory : MonoBehaviour
{
    public static TwisterWarningFactory instance;
    Pool<TwisterWarning> _twisterWarningPool;
    public int initialAmount;
    [SerializeField] TwisterWarning _twisterWarning;
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

        _twisterWarningPool = new Pool<TwisterWarning>(CreathorMethod, TwisterWarning.TurnOnCallBack, TwisterWarning.TurnOffCallBack, initialAmount);
    }

    TwisterWarning CreathorMethod()
    {
        return Instantiate(_twisterWarning);
    }
    public TwisterWarning GetObjFromPool()
    {
        return _twisterWarningPool.GetObj();
    }

    public void ReturnToPool(TwisterWarning twisterWarning)
    {
        _twisterWarningPool.Return(twisterWarning);
    }
}
