using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement 
{
    Rigidbody _rb;
    Player_Inputs _inputs;
    float _jumpForce;
    public bool jump = true;
    int _speed;
    float _dashForce;
    float _dashDuration;
    float _dashTimer;
    float _dashTimer2;
    float _dashCooldown;
    bool _dashing;
    
    float _glideDrag;
    float _initialDrag = 0.1f;
    bool _gliding = false;
    Transform _transform;
    
    public Player_Movement(Rigidbody rb , Player_Inputs inputs , int speed , float jumpForce , float dashForce , float dashDuration , float dashCooldown , Transform transform ,float glideDrag)
    {
        _rb = rb;
        _inputs = inputs;
        _speed = speed;
        _jumpForce = jumpForce;
        _dashForce = dashForce;
        _dashDuration = dashDuration;
        _dashCooldown = dashCooldown;
        _transform = transform;
        _glideDrag = glideDrag;
    }

    public void ArtificialStart()
    {
        _rb.drag = _initialDrag;
    }

    public void ArtificialUpdate()
    {
        _dashTimer2 += Time.deltaTime;
        Move();
    }


    void Move()
    {
        if (_dashing)
        {
            _dashTimer += Time.deltaTime;

            if (_dashTimer>=_dashDuration)
            {
                _dashing = false;
                _dashTimer = 0;
            }
        }
        else
        {
            //_rb.MovePosition(_rb.position+_inputs.direction * _speed * Time.deltaTime);
            _transform.position += _inputs.axis * _speed * Time.deltaTime;
        }
        
    }

    public void Jump()
    {
        if (jump)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
           jump = false;
        }
    }

    public void Dash()
    {
        if (_dashTimer2>=_dashCooldown)
        {
            _dashing = true;
            _rb.AddForce(_inputs.axis.normalized * _dashForce, ForceMode.Impulse);
            _dashTimer2 = 0;

        }
    }
    public void Glide()
    {
        if (jump==false && _gliding==false)
        {
            _rb.drag = _glideDrag;
            //_gliding = true;
        }

       /* if (jump==false && _gliding==true)
        {
            _rb.drag = _initialDrag;
            _gliding = false;
        }*/
    }
}
