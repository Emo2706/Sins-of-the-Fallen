using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeftZone : MonoBehaviour
{
    public static event Action LeftZoneEvent = delegate { };


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            LeftZoneEvent();

            gameObject.SetActive(false);
        }
    }
}
