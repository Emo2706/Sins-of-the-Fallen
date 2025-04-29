using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;
using System.Diagnostics;

public class CameraExtraFunctions
{
    /*Camera _cam;
    float _initialFOV;
    float _FOVGoesTo;
    Sequence _FOVDashSequence;
    float _dashCamTransition = 0.1f;
    float _camRotationMultiplier;
    float _maxRotation = 50;
    Sequence camRotationSequ;
    Sequence _camRotationSideToSideSequence;

    public CameraExtraFunctions(Camera camera, float fovGoesto, float camRotMultiplier)
    {
        _cam = camera;
        _initialFOV = _cam.fieldOfView;
        _FOVGoesTo = fovGoesto;
        _camRotationMultiplier = camRotMultiplier;

    }
    public void FovDash(float duration)
    {
        _FOVDashSequence.Kill();
        float transition = 0.05f;
        _FOVDashSequence = DOTween.Sequence();
        _FOVDashSequence.Append(_cam.DOFieldOfView(_FOVGoesTo, transition));
        _FOVDashSequence.AppendInterval(duration - transition);
        _FOVDashSequence.Append(_cam.DOFieldOfView(_initialFOV, _dashCamTransition));

    }
    public void RotateCam(int moveDirection, float speed, float force)
    {
        _camRotationSideToSideSequence.Kill();
        _camRotationSideToSideSequence = DOTween.Sequence();
        _camRotationSideToSideSequence.Append(DOTween.To(() => FirstPersonCamera.instance.camZ, x => FirstPersonCamera.instance.camZ = x, moveDirection * force, speed));
      
    }
    public void RotateCamOnce(float force, float camShakeDuration = 0.2f)
    {
        _cam.transform.DOBlendablePunchRotation(_cam.transform.forward * force, camShakeDuration, 1);


    }

    public void ShakeCam(float force, float camShakeDuration = 0.2f, int vibrato = 50)
    {

        Vector3 goBack = new Vector3(_cam.transform.rotation.x, _cam.transform.rotation.y, _cam.transform.rotation.z);
        camRotationSequ.Kill();

        _cam.transform.DOBlendablePunchRotation(_cam.transform.right * force, camShakeDuration, vibrato).SetEase(Ease.InSine);



        Debug.Log("Shake");
        Debug.Log(force);
        Debug.Log(camShakeDuration);
        Debug.Log(vibrato);
    }*/
}
