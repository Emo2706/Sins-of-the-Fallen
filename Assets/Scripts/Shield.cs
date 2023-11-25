using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public static void TurnOffCallBack(Shield shield)
    {
        shield.gameObject.SetActive(false);
    }

    public static void TurnOnCallBack(Shield shield)
    {
        shield.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==10)
            AudioManager.instance.Play(AudioManager.Sounds.HitShield);

    }
}
