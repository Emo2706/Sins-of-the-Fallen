using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotGlideInput : CommandInputs
{
    Player_Movement _movement;
    public NotGlideInput(Player_Movement movement)
    {
        _movement = movement;
    }

    public override void Execute()
    {
        _movement.NotGlide();
    }
}
