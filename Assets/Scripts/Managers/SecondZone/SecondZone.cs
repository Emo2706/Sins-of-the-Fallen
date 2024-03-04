using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SecondZone : MonoBehaviour
{
    public static event Action SecondZoneEvent = delegate { };


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            SecondZoneEvent();

            gameObject.SetActive(false);
        }
    }
}
