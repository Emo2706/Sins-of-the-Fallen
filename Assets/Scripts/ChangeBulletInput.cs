using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBulletInput : CommandInputs
{
    Player_Attacks _attacks;

    public ChangeBulletInput(Player_Attacks attacks)
    {
        _attacks = attacks;
    }
    public override void Execute()
    {
        _attacks.ChangeBullet();
    }
}
