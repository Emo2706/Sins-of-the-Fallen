using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePotion : PowerUp
{
    [SerializeField] int _lifeTime;
    float _lifeTimer;

    // Update is called once per frame
    void Update()
    {
        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _lifeTime) LifePotionFactory.instance.ReturnToPool(this);
    }

    private void Reset()
    {
        _lifeTimer = 0;
    }

    public static void TurnOffCallBack(LifePotion potion)
    {
        potion.Reset();
        potion.gameObject.SetActive(false);
    }


    public static void TurnOnCallBack(LifePotion potion)
    {
        potion.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) LifePotionFactory.instance.ReturnToPool(this);
    }

    public override void Active()
    {
        //
    }
}
