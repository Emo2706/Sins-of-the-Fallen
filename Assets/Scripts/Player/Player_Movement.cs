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
    Ray _movRay;
    float _movRange;
    LayerMask _moveMask;
    Vector3 _dir;
    float _sensitivity;
    float _mouseX;
    FirstPersonCamera _cam;
    
    public Player_Movement(Rigidbody rb , Player_Inputs inputs , int speed , float jumpForce , float dashForce , float dashDuration , float dashCooldown , Transform transform ,float glideDrag , LifeHandler lifeHandler , int slimeForce , Player player , FirstPersonCamera cam)
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
        _cam = cam;
        _moveMask = player.movMask;
        _sensitivity = player.sensitivity;
        _movRange = player.movRange;
    }

    public void ArtificialStart()
    {
        _rb.drag = _initialDrag;
        _lifeHandler.onDeath += DisableOnDead;
    }

    public void Update()
    {
        _dashTimer2 += Time.deltaTime;
        _dir = (_transform.right * _inputs.axis.x + _transform.forward*_inputs.axis.z).normalized;

    }

    public void FixedUpdate()
    {

        if (IsBlocked(_dir) == false)
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
            _transform.position += _dir* _speed * Time.fixedDeltaTime;
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


    bool IsBlocked(Vector3 dir)
    {
        _movRay = new Ray(_transform.position, dir);

        return Physics.Raycast(_movRay, _movRange, _moveMask);
    }

    public void Rotation(float x , float y)
    {
        _mouseX += x * _sensitivity * Time.deltaTime;

        if(_mouseX>=360 || _mouseX <= -360)
        {
            _mouseX -= 360 * Mathf.Sign(_mouseX);
        }

        y *= _sensitivity * Time.deltaTime;

        _transform.rotation = Quaternion.Euler(0f, _mouseX, 0f);

        _cam?.Rotation(_mouseX, y);
    }

}
