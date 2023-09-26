using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGlobalScript : Entity
{
    Enemy_Stats _enemy_stats;

    // Start is called before the first frame update
    void Start()
    {
        _life = _maxLife;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        _life = _maxLife;
    }

    public static void TurnOnCallBack(EnemyGlobalScript enemy)
    {
        enemy.Reset();
        enemy.gameObject.SetActive(true);
    }

    public static void TurnOffCallBack(EnemyGlobalScript enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    public override void CheckLife()
    {
        base.CheckLife();

        if (_life<=0)
        {
            EnemyFactory.instance.ReturnToPool(this);
        }
    }
}
