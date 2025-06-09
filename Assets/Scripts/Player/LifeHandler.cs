using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeHandler
{
    public event Action onDeath = delegate { };
    

    public LifeHandler()
    {
       
    }
    
    public void OnDead()
    {
        onDeath();
        ScreenManager.instance.Push("LostScreen");
        EventManager.TriggerEvent(EventManager.EventsType.Event_PlayerDead);
        Debug.Log("OnDead");
    }
}
