using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    public Transform player;
    public float minRotX, maxRotX;
    public float speed;
    float _rotX;


    private void Update()
    {
        Look();
    }
    void Look()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        player.Rotate(Vector3.up * x * speed);
        
        

        _rotX -= y * speed;
        _rotX = Mathf.Clamp(_rotX, minRotX, maxRotX);
        transform.localEulerAngles = Vector3.right * _rotX;
        //player.transform.localRotation = Quaternion.Euler(_rotX, 0f, 0f);
        
    }
    
    
    
    
    /*public Transform playerCamera;
    Vector3 rotationinput;
    float rotationSensibility=100f;
    float cameraVerticalAngle;
    float _minRotX = -70f;
    float _maxRotX = 70f;
    public float speed;

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        rotationinput.x = Input.GetAxis("Mouse X") * rotationSensibility * Time.deltaTime;
        rotationinput.y = Input.GetAxis("Mouse Y") * rotationSensibility * Time.deltaTime;

        cameraVerticalAngle = cameraVerticalAngle + rotationinput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, _minRotX, _maxRotX);

        playerCamera.Rotate(Vector3.up * rotationinput.x * speed);
        playerCamera.transform.localRotation = Quaternion.Euler(-cameraVerticalAngle, 0f, 0f);
    }*/
}

