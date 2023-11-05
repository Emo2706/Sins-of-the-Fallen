using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] float _minRotation = -45f;
    [SerializeField] float _maxRotation = 45f;

    float _mouseY;

    Transform _head;

    public Transform head { get { return _head; } set { _head = value; } }

    private void LateUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.position = _head.position;
    }

    public void Rotation(float x , float y)
    {
        _mouseY += y;
        _mouseY = Mathf.Clamp(_mouseY, _minRotation, _maxRotation);

        transform.rotation = Quaternion.Euler(-_mouseY, x, 0f);
    }
}
