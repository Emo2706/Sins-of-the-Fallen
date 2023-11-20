using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
            EventManager.TriggerEvent(EventManager.EventsType.Event_WinGame);
    }
}
