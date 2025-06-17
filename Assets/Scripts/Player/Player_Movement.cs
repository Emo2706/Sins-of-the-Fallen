using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public bool _canGlide;
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
        _player.enabled = true;
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
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
            }
        }
        else
        {
            _rb.MovePosition(_rb.position + _dir * _speed * Time.fixedDeltaTime);
            //_rb.velocity = _rb.position + _dir * _speed * Time.fixedDeltaTime;
            //_transform.position += _dir* _speed * Time.fixedDeltaTime;
        }
        
    }

    public void Jump()
    {
        if (jump)
        {
            
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            jump = false;
            _player.StartCoroutine(GlideEnable());
            AudioManager.instance.PlayRandom(new int[] { AudioManager.Sounds.Jump1, AudioManager.Sounds.Jump2, AudioManager.Sounds.Jump3, AudioManager.Sounds.Jump4, AudioManager.Sounds.Jump5 });
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
        _rb.AddForce(Vector3.back * (_twisterForce/3), ForceMode.Impulse);
        jump = false;
    }

    public void Dash()
    {
        if (_dashTimer2>=_dashCooldown && _dir != Vector3.zero)
        {
            if(_rb.drag == _initialDrag)
            {
                _dashing = true;
                _rb.AddForce(_dir * _dashForce, ForceMode.Impulse);
                _dashTimer2 = 0;
                _player.dashParticles.SetActive(true);
                _player.dashWind.SetActive(true);
                _player.StartCoroutine(DashEffects());
            }

        }

    }

    /*public void Dash()
    {
        if (_stunt) return;

        _player.StartCoroutine(DashCoroutine(_playerDir));
    }*/

    public void Glide()
    {
        if (jump==false && _canGlide == true)
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

    IEnumerator GlideEnable()
    {
        yield return new WaitForSeconds(0.5f);
        _canGlide = true;
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

    IEnumerator DashEffects()
    {
        float timer = 0;
        while(timer < _dashDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _player.dashParticles.SetActive(false);
        _player.dashWind.SetActive(false);
    }

    /*IEnumerator DashCoroutine(Vector3 dir)
    {
        if (canDashFromTutorial)
        {
            
            FirstPersonCamera.instance.cameraFunctions.FovDash(_dashDuration);
            HUDManager.instance.hUDHabilities.habilities[0].Used(_legData.dashCooldown);
            FullScreenShaderManager.instance.Dash();
            _player.ApplyHeat(_legData.dashHeatExposure);
            AudioManager.instance.PlayRandom(new int[] { AudioManager.Sounds.Dash1, AudioManager.Sounds.Dash2, AudioManager.Sounds.Dash3 });
            MissionManager.instance.AddProgressToThisMission(MissionManager.MisionType.DashInAroom, 1);
            bool wasInTheAirBeforeDashing = !jump;

            isDashing = true;
            ResetJetpack();
            JetpackMethod = delegate { };
            if (!jump)
            {
                _cf.enabled = false;

            }
            else _cf.enabled = true;
            _rb.useGravity = !_rb.useGravity;
            _rb.velocity = FlyweightImportants.instance.MainCam.transform.forward * _dashForce;
            //if (dir == Vector3.zero) _rb.velocity = Camera.main.transform.forward * _dashForce;

            //else _rb.velocity = (_transform.right * dir.x + _transform.forward * dir.z).normalized * _dashForce;

            yield return new WaitForSeconds(_dashDuration);
            if (wasInTheAirBeforeDashing || jump)
                _rb.velocity = Vector3.zero;
            else _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _dashForce / 2);


            yield return new WaitForSeconds(0.3f);
            _rb.useGravity = !_rb.useGravity;
            JetpackMethod = JetpackFlying;


            yield return new WaitForSeconds(_legData.dashCooldown);
            _cf.enabled = true;

            isDashing = false;
        }
    }*/

}
