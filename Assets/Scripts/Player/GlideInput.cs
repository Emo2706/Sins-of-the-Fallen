using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideInput : CommandInputs
{
    Player_Movement _movement;

    public GlideInput(Player_Movement movement)
    {
        _movement = movement;
    }
    public override void Execute()
    {
        if(!_movement.jump)
        _movement.Glide();
    }
}
