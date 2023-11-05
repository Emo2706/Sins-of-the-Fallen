using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderFirstZone : MonoBehaviour
{
    public static event Action FirstZone = delegate { };


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            FirstZone();

            gameObject.SetActive(false);
        }
    }

}
