using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement 
{
    Rigidbody _rb;
    Player_Inputs _inputs;
    float _jumpForce;
    public bool jump;
    int _speed;

    
    public Player_Movement(Rigidbody rb , Player_Inputs inputs , int speed , float jumpForce)
    {
        _rb = rb;
        _inputs = inputs;
        _speed = speed;
        _jumpForce = jumpForce;
    }

    public void ArtificialUpdate()
    {
        Move();
    }


    void Move()
    {
        _rb.MovePosition(_rb.position+_inputs.direction * _speed * Time.deltaTime);
    }

    public void Jump()
    {
        if (jump)
        {
            jump = false;
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
        }
    }
}
