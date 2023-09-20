using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Collisions 
{
    Player_Movement _movement;

    public Player_Collisions(Player_Movement movement)
    {
        _movement = movement;
    }

    public void ArtificialOnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
            _movement.jump = true;

        
    }
}
