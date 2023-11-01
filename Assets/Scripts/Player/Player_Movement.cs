using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement 
{
    Rigidbody _rb;
    Player_Inputs _inputs;
    LifeHandler _lifeHandler;
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
    float _initialDrag = 0.05f;
    Transform _transform;
    int _slimeForce;
    float _twisterForce;
    Player _player;
    
    public Player_Movement(Rigidbody rb , Player_Inputs inputs , int speed , float jumpForce , float dashForce , float dashDuration , float dashCooldown , Transform transform ,float glideDrag , LifeHandler lifeHandler , int slimeForce , Player player)
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
        _lifeHandler = lifeHandler;
        _slimeForce = slimeForce;
        _player = player;
        _twisterForce = _player.twisterForce;
    }

    public void ArtificialStart()
    {
        _rb.drag = _initialDrag;
        _lifeHandler.onDeath += DisableOnDead;
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
            _transform.position += _inputs.axis * _speed * Time.fixedDeltaTime;
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

    public void JumpSlime()
    {
        _rb.AddForce(Vector3.up * _slimeForce, ForceMode.Impulse);
        jump = false;
    }

    public void Twister()
    {
        _rb.AddForce(Vector3.up * _twisterForce, ForceMode.Impulse);
        jump = false;
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
        if (jump==false)
        {
            _rb.drag = _glideDrag;
            
        }

    }

    public void NotGlide()
    {
        if (jump==false) _rb.drag = _initialDrag;


    }

    void DisableOnDead()
    {
        _player.enabled = false;
    }


}
