using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePotion : PowerUp
{
    [SerializeField] int _lifeTime;
    [SerializeField] int _lifePotion;
    float _lifeTimer;

    Player _player;

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

        _player = other.GetComponent<Player>();
    }



    public override void Active()
    {
        if (_player != null)
        {
            _player.life += _lifePotion;
            AudioManager.instance.Play(AudioManager.Sounds.LifePotion);

        }
    }
}
