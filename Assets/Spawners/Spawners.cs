using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    public Transform spawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            var Enemy = EnemyFactory.instance.GetObjFromPool();
            Enemy.gameObject.transform.position = spawn.position;

        }
    }
}
