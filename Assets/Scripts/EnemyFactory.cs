using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory instance { get; private set; }
    Pool<EnemyGlobalScript> _enemyPool;
    [SerializeField] EnemyGlobalScript _enemy;
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

        _enemyPool = new Pool<EnemyGlobalScript>(CreatorMethod , EnemyGlobalScript.TurnOnCallBack, EnemyGlobalScript.TurnOnCallBack, _initialAmount);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    EnemyGlobalScript CreatorMethod()
    {
        return Instantiate(_enemy);
    }

    public EnemyGlobalScript GetObjFromPool()
    {
       return _enemyPool.GetObj();
    }

    public void ReturnToPool(EnemyGlobalScript enemy)
    {
        _enemyPool.Return(enemy);
    }

   
}
