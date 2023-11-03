using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPowerUp : PowerUp
{
    [SerializeField] Player _player;
   

    public override void Active()
    {
        _player.PowerUpBullet();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
            gameObject.SetActive(false);
    }

}
