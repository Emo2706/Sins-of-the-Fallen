using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeHandlerBoss
{
    public event Action OnHalfLife = delegate {};

    public LifeHandlerBoss()
    {

    }

    public void HalfLife()
    {
        //OnHalfLife();
        EventManager.TriggerEvent(EventManager.EventsType.Event_BossHalfLife);
    }
    
}
