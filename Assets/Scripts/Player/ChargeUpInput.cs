using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeUpInput : CommandInputs
{
    Player_Attacks _attacks;

    public ChargeUpInput(Player_Attacks attacks)
    {
        _attacks = attacks;
    }

    public override void Execute()
    {
        _attacks.ChargeUp();
    }
}
