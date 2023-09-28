using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inputs 
{
    public Vector3 direction;
    public Vector3 axis;
    Transform _transform;

    Dictionary<KeyCode,CommandInputs> _commandDictionary;

    public Player_Inputs(Transform transform)
    {
        _transform = transform;
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
            if (Input.GetKeyUp(pair.Key))
            {

            }
        }

        return null;
    }

    public void ArtificialUpdate()
    {
        axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (axis.magnitude > 1) axis = _transform.TransformDirection(axis).normalized;
        else axis = _transform.TransformDirection(axis);


        /*direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");*/

    }
}
