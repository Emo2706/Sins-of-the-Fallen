using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttackFactory : MonoBehaviour
{
    public static CircleAttackFactory instance;
    Pool<CircleAttack> _circleAttackPool;
    public int initialAmount;
    [SerializeField] CircleAttack _circleAttack;
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

        _circleAttackPool = new Pool<CircleAttack>(CreathorMethod, CircleAttack.TurnOnCallBack, CircleAttack.TurnOffCallBack, initialAmount);
    }

    CircleAttack CreathorMethod()
    {
        return Instantiate(_circleAttack);
    }
    public CircleAttack GetObjFromPool()
    {
        return _circleAttackPool.GetObj();
    }

    public void ReturnToPool(CircleAttack circleAttack)
    {
        _circleAttackPool.Return(circleAttack);
    }
}
