using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyers : EnemyGlobalScript
{
    public static EnemyFlyers instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    

    public static void TurnOnCallBack(EnemyFlyers enemy)
    {
        enemy.Reset();
        enemy.gameObject.SetActive(true);
    }

    public static void TurnOffCallBack(EnemyFlyers enemy)
    {
        enemy.gameObject.SetActive(false);
    }
}
