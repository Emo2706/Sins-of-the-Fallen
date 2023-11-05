using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nine : Weapon
{
    protected override void ShotBehaviour()
    {
        _shootRay = new Ray(_barrelTransform.position, _barrelTransform.forward);

        if(Physics.Raycast(_shootRay, out _shootRayHit, _shootRange, _shootMask))
        {
            print($"<color=orange>9mm hit {_shootRayHit.collider.name}.</color>");
        }
    }
}
