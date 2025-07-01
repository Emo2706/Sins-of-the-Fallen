using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : MonoBehaviour
{
    [SerializeField] int _lifeTime;
    [SerializeField] int _speedIncrease;
    [SerializeField] int _initialScaleY;
    Vector3 _scaleIncrease;
    float _lifeTimer;
    Vector3 _initialScale;

    void Start()
    {
        AudioManager.instance.Play(AudioManager.Sounds.CircleAttack);
    }
    // Update is called once per frame
    void Update()
    {

        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _lifeTime)
            CircleAttackFactory.instance.ReturnToPool(this);

        ScaleIncrease();

    }

    void ScaleIncrease()
    {
        _scaleIncrease = transform.localScale;

        _scaleIncrease.x += Time.deltaTime * _speedIncrease;
        _scaleIncrease.z += Time.deltaTime * _speedIncrease;

        transform.localScale = _scaleIncrease;
    }

    private void Reset()
    {
        _lifeTimer = 0;
        transform.localScale = _initialScale;
    }

    public static void TurnOnCallBack(CircleAttack circle)
    {
        circle.Reset();
        circle.gameObject.SetActive(true);
    }


    public static void TurnOffCallBack(CircleAttack circle)
    {
        circle.gameObject.SetActive(false);
    }
}
