using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwisterAttackFactory : MonoBehaviour
{
    public static TwisterAttackFactory instance;
    Pool<TwisterAttack> _twisterAttackPool;
    public int initialAmount;
    [SerializeField] TwisterAttack _twisterAttack;
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

        _twisterAttackPool = new Pool<TwisterAttack>(CreathorMethod, TwisterAttack.TurnOnCallBack, TwisterAttack.TurnOffCallBack, initialAmount);
    }

    TwisterAttack CreathorMethod()
    {
        return Instantiate(_twisterAttack);
    }
    public TwisterAttack GetObjFromPool()
    {
        return _twisterAttackPool.GetObj();
    }

    public void ReturnToPool(TwisterAttack twisterAttack)
    {
        _twisterAttackPool.Return(twisterAttack);
    }
}
