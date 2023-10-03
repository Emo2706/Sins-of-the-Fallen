using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : EnemyGlobalScript
{
    public static EnemyShooter instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void TurnOnCallBack(EnemyShooter enemy)
    {
        enemy.Reset();
        enemy.gameObject.SetActive(true);
    }
    public static void TurnOffCallBack(EnemyShooter enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    public override void TakeDmg(int dmg)
    {
        base.TakeDmg(dmg);

        CheckLife();
    }

     void CheckLife()
     {
        if (_life <= 0)
            EnemyShooterFactory.instance.ReturnToPool(this);
        
        
     }
}
