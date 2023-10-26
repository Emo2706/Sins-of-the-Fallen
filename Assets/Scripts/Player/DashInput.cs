using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashInput : CommandInputs
{
    Player_Movement _movement;

    public DashInput(Player_Movement movement)
    {
        _movement = movement;
    }

    public override void Execute()
    {
        _movement.Dash();
    }
}
