using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LifeHandlerBoss
{
    public LifeHandlerBoss()
    {

    }

    public void Defeated()
    {
        //OnHalfLife();
        EventManager.TriggerEvent(EventManager.EventsType.Event_BossDefeated);
    }
    
}
