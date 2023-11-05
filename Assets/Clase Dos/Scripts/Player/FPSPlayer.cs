using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FPSPlayer : MonoBehaviour
{
    [Header("Camera")]
    [Range(1f, 1000f)][SerializeField] private float _mouseSensitivity = 100f;

    [Header("Components")]
    [SerializeField] private Transform _headTransform;

    [Header("Inputs")]
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;
    [SerializeField] private KeyCode _shootKey = KeyCode.Mouse0;

    [Header("Values")]
    [SerializeField] private float _movRange = .75f;
    [SerializeField] private float _movSpeed = 5f;
    [SerializeField] private LayerMask _movMask;

    [Header("Weapons")]
    [SerializeField] private List<Weapon> _weaponInventory;
    [SerializeField] private LayerMask _weaponMask;

    private float _xAxis, _zAxis, _inputMouseX, _mouseX, _inputMouseY, _mouseWheel;
    private int _currentWeaponIndex;
    private Vector3 _dir;

    private FPSCamera _cam;
    private Rigidbody _rb;
    private Weapon _currentWeapon;

    private Ray _movRay;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Start()
    {
        _cam = Camera.main.GetComponent<FPSCamera>();
        _cam.Head = _headTransform;

        #region Weapon
        if (_weaponInventory.Count > 0)
        {
            _currentWeapon = _weaponInventory[0];

            foreach (Weapon weapon in _weaponInventory)
            {
                weapon.SetParams(_headTransform, _weaponMask);

                if (weapon != _currentWeapon) weapon.gameObject.SetActive(false);
            }
        }  
        #endregion
    }

    private void Update()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _zAxis = Input.GetAxis("Vertical");

        _dir = (transform.right * _xAxis + transform.forward * _zAxis).normalized;

        _inputMouseX = Input.GetAxisRaw("Mouse X");
        _inputMouseY = Input.GetAxisRaw("Mouse Y");
        _mouseWheel += Input.GetAxis("Mouse ScrollWheel");

        if(_inputMouseX != 0 || _inputMouseY != 0)
        {
            Rotation(_inputMouseX, _inputMouseY);
        }

        #region Weapon
        if (_currentWeapon)
        {
            if (Input.GetKeyDown(_shootKey) && _currentWeapon.CanShoot)
            {
                _currentWeapon.Shoot();
            }
            else if (Input.GetKeyDown(_reloadKey) && !_currentWeapon.IsReloading)
            {
                _currentWeapon.Reload();
            }
            else if (_mouseWheel > .25f || _mouseWheel < -.25f)
            {
                if (_mouseWheel > 0f)
                {
                    ChangeWeapon(1);
                }
                else
                {
                    ChangeWeapon(-1);
                }
            }
        } 
        #endregion
    }

    private void FixedUpdate()
    {
        if((_xAxis != 0 || _zAxis != 0) && !IsBlocked(_dir))
        {            
            Movement(_dir);
        }
    }

    private void Movement(Vector3 dir)
    {
        _rb.MovePosition(transform.position + dir * _movSpeed * Time.fixedDeltaTime);
    }

    private void Rotation(float x, float y)
    {
        _mouseX += x * _mouseSensitivity * Time.deltaTime;

        if(_mouseX >= 360 || _mouseX <= -360)
        {
            _mouseX -= 360 * Mathf.Sign(_mouseX);
        }

        y *= _mouseSensitivity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0f, _mouseX, 0f);

        _cam?.Rotation(_mouseX, y);
    }

    private bool IsBlocked(Vector3 dir)
    {
        _movRay = new Ray(transform.position, dir);

        return Physics.Raycast(_movRay, _movRange, _movMask);
    }

    #region Weapon
    private void ChangeWeapon(int value)
    {
        _mouseWheel = 0f;

        _currentWeaponIndex += value;

        if (_currentWeaponIndex >= _weaponInventory.Count)
        {
            _currentWeaponIndex = 0;
        }
        else if (_currentWeaponIndex < 0)
        {
            _currentWeaponIndex = _weaponInventory.Count - 1;
        }
        _currentWeapon.gameObject.SetActive(false);
        _currentWeapon = _weaponInventory[_currentWeaponIndex];
        _currentWeapon.gameObject.SetActive(true);
    } 
    #endregion
}
