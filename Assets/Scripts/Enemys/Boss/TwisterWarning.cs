using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwisterWarning : MonoBehaviour
{
    float _lifeTimer;
    [SerializeField] int _lifeTime;
    

    // Update is called once per frame
    void Update()
    {
        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _lifeTime)
            TwisterWarningFactory.instance.ReturnToPool(this);
    }

    private void Reset()
    {
        _lifeTimer = 0;
    }

    public static void TurnOnCallBack(TwisterWarning warning)
    {
        warning.Reset();
        warning.gameObject.SetActive(true);
    }


    public static void TurnOffCallBack(TwisterWarning warning)
    {
        warning.gameObject.SetActive(false);
    }
}
