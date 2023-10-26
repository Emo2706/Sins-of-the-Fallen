using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attacks
{
    Transform _transform;
    public Player_Attacks(Transform transform)
    {
        _transform = transform;
    }
    public void Shoot()
    {
        var bullet = BulletFactory.instance.GetObjFromPool();
        bullet.transform.position = _transform.position;
        bullet.dir = _transform.forward;
    }
}
