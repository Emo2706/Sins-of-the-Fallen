using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attacks
{
    Transform _transform;
    Player_Inputs _inputs;
    public Player_Attacks(Transform transform , Player_Inputs inputs)
    {
        _transform = transform;
        _inputs = inputs;
    }
    public void Shoot()
    {
        var bullet = BulletFactory.instance.GetObjFromPool();
        bullet.transform.position = _transform.position;
        bullet.dir = _transform.forward;
    }
}
