using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    Pool<EnemyGlobalScript> _enemyPool;
    [SerializeField] EnemyGlobalScript _enemy;
    [SerializeField] int _initialAmount;

    private void Awake()
    {
        _enemyPool = new Pool<EnemyGlobalScript>(CreatorMethod , TurnOnCallBack, TurnOnCallBack, _initialAmount);
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

    public void TurnOnCallBack(EnemyGlobalScript enemy)
    {

    }

    public void TurnOffCallBack(EnemyGlobalScript enemy)
    {

    }
}
