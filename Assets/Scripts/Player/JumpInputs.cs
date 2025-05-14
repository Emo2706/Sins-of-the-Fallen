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
        if (_movement.jump)
        {
            _movement.Jump();
            return;
        }
        else
        {
            //_movement.Glide();
        }
        
    }
    

    
}
