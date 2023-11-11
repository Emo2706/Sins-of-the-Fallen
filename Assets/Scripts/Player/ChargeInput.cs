using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeInput : CommandInputs
{
    Player_Attacks _attacks;

    public ChargeInput(Player_Attacks attacks)
    {
        _attacks = attacks;
    }

    public override void Execute()
    {
        _attacks.ChargeBullet();
    }

 
}
