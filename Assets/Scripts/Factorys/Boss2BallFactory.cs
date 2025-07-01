using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2BallFactory : MonoBehaviour
{
    public static Boss2BallFactory instance;
    Pool<BossBall> _bossBallPool;
    public int initialAmount;
    [SerializeField] BossBall _ballPrefab;
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

        _bossBallPool = new Pool<BossBall>(CreathorMethod, BossBall.TurnOnCallBack, BossBall.TurnOffCallBack, initialAmount);
    }

    BossBall CreathorMethod()
    {
        return Instantiate(_ballPrefab);
    }
    public BossBall GetObjFromPool()
    {
        return _bossBallPool.GetObj();
    }

    public void ReturnToPool(BossBall ball)
    {
        _bossBallPool.Return(ball);
    }
}
