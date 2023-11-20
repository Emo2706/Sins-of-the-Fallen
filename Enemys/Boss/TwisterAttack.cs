using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwisterAttack : MonoBehaviour
{
    float _lifeTimer;
    [SerializeField] int _lifeTime;
 
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.pause) return;

        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _lifeTime)
            TwisterAttackFactory.instance.ReturnToPool(this);
    }


    private void Reset()
    {
        _lifeTimer = 0;
    }

    public static void TurnOnCallBack(TwisterAttack attack)
    {
        attack.Reset();
        attack.gameObject.SetActive(true);
    }


    public static void TurnOffCallBack(TwisterAttack attack)
    {
        attack.gameObject.SetActive(false);
    }
}
