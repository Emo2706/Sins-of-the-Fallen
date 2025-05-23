using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    [Header("Clamping")]
    [SerializeField] private float _minRotation = -45f;
    [SerializeField] private float _maxRotation = 45f;
    [SerializeField] private float _duration;
    [SerializeField] private float _magnitude;

    private float _mouseY;

    private Transform _head;
    public Transform Head { get { return _head; } set { _head = value; } }

    private void LateUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        transform.position = _head.position;
    }

    public void Rotation(float x, float y)
    {
        _mouseY += y;
        _mouseY = Mathf.Clamp(_mouseY, _minRotation, _maxRotation);

        transform.rotation = Quaternion.Euler(-_mouseY, x, 0f);
    }

   
}
