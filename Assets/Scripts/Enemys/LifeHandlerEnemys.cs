using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeHandlerEnemys : MonoBehaviour
{
    public event Action OnDeadEvent = delegate { };

    public void OnDead()
    {
        OnDeadEvent();
        EventManager.TriggerEvent(EventManager.EventsType.Event_EnemyDead);
    }
}
