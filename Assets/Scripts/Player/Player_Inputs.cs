using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inputs 
{
    public Vector3 direction;
    public Vector3 axis;
    Transform _transform;
    LifeHandler _lifeHandler;
    Player _player;
    Player_Movement _movement;
    public Vector3 finalMousePos;
    float _mouseX;
    float _mouseY;

    Dictionary<KeyCode,CommandInputs> _commandDictionary;

    Dictionary<KeyCode, CommandInputs> _commandUpDictionary;

    public Player_Inputs(Transform transform , LifeHandler lifeHandler , Player player)
    {
        _transform = transform;
        _commandDictionary = new Dictionary<KeyCode, CommandInputs>();
        _commandUpDictionary = new Dictionary<KeyCode, CommandInputs>();
        _lifeHandler = lifeHandler;
        _player = player;
    }

    public void CompleteData(Player_Movement movement)
    {
        _movement = movement;
    }

    public void ArtificialStart()
    {
        _lifeHandler.onDeath += DisableOnDead;
    }

    public void BlindKeys(KeyCode key, CommandInputs command)
    {
        _commandDictionary[key] = command;
    }

    public void BlindKeysUp(KeyCode key ,CommandInputs command)
    {
        _commandUpDictionary[key] = command;
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

        foreach(var pair in _commandUpDictionary)
        {
            if (Input.GetKeyUp(pair.Key))
            {
                return pair.Value;
            }
        }

        return null;
    }

    public void ArtificialUpdate()
    {
        //axis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //if (axis.magnitude > 1) axis = _transform.TransformDirection(axis).normalized;
        //else axis = _transform.TransformDirection(axis);

        axis.x = Input.GetAxisRaw("Horizontal");
        axis.z = Input.GetAxisRaw("Vertical");

        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _movement.Rotation(_mouseX, _mouseY);

    }

    void DisableOnDead()
    {
        _player.enabled = false;
    }
}
