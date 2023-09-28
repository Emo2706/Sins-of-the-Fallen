using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyerSpawner : MonoBehaviour
{
    public Transform[] transforms;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < transforms.Length; i++)
        {
            var enemy = EnemyFlyersFactory.instance.GetObjFromPool();
            enemy.transform.position = transforms[i].position;
        }
    }

    
}
