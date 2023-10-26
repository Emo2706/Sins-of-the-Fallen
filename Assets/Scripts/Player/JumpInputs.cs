using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpInputs : CommandInputs
{
    Player_Movement _movement;

    public JumpInputs(Player_Movement movement)
    {
        _movement = movement;
    }

    public override void Execute()
    {
        _movement.Jump();
    }
    

    
}
