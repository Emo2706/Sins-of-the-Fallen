using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] transforms;
    
    private void Start()
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            var enemy = EnemyFactory.instance.GetObjFromPool();
            enemy.transform.position = transforms[i].position;
        }
    }


}
