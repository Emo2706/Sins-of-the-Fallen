using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inputs 
{
    public Vector3 direction;

    Dictionary<KeyCode,CommandInputs> _commandDictionary;

    public Player_Inputs()
    {
        _commandDictionary = new Dictionary<KeyCode, CommandInputs>();
    }

    public void BlindKeys(KeyCode key, CommandInputs command)
    {
        _commandDictionary[key] = command;
    }

    public CommandInputs Inputs()
    {
        foreach (var pair in _commandDictionary)
        {
            if (Input.GetKeyDown(pair.Key))
            {
                return pair.Value;
            }
        }

        return null;
    }

    public void ArtificialUpdate()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

    }
}
