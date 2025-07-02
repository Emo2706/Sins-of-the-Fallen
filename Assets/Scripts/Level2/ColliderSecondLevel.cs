using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderSecondLevel : MonoBehaviour
{
    public static event Action StartBoss = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            StartBoss();
            AudioManager.instance.Stop(AudioManager.Sounds.Ambience);
            AudioManager.instance.Play(AudioManager.Sounds.MusicMiniboss);
        }
    }
}
