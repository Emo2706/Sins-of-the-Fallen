using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] float _minRotation = -45f;
    [SerializeField] float _maxRotation = 45f;
    [SerializeField] float _duration = 1.5f;
    [SerializeField] float _magnitude = 2.5f;

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


    public IEnumerator ShakeCourotine(float duration , float magnitude)
    {
        Vector3 originalPosition = _head.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;

            float y = Random.Range(-1f, 1f) * magnitude;

            _head.transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        _head.transform.localPosition = originalPosition;
    }
}
