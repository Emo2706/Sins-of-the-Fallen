using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

public class FirstPersonCamera : MonoBehaviour
{
    //public static FirstPersonCamera instance;
    [SerializeField] float _minRotation = -45f;
    [SerializeField] float _maxRotation = 45f;
    //[SerializeField] float _duration = 1.5f;
    //[SerializeField] float _magnitude = 2.5f;
    //[SerializeField] Transform _initialView;
    //public CameraExtraFunctions cameraFunctions;
    //[SerializeField] float _fovInDashGoesTo;
    //[SerializeField] float _camRotationMultiplier;

    float _mouseY;

    Transform _head;

    public Transform head { get { return _head; } set { _head = value; } }

    /*private void Awake()
    {
        if (instance == null) instance = this;

        transform.LookAt(_initialView);
        cameraFunctions = new CameraExtraFunctions(FlyweightImportants.instance.MainCam, _fovInDashGoesTo, _camRotationMultiplier);
    }*/

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
