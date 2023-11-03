using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public enum EventsType
    {
        Event_PlayerDead
    }

    public delegate void EventsReceiver(params object[] parameters);

   static Dictionary<EventsType, EventsReceiver> _events;

    public static void SubscribeToEvent(EventsType eventType , EventsReceiver listener)
    {
        if (_events == null) _events = new Dictionary<EventsType, EventsReceiver>();

        if (!_events.ContainsKey(eventType))
        {
            _events.Add(eventType, null);
        }

        _events[eventType] += listener;
    }

    public static void UnSubscribeToEvent(EventsType eventsType , EventsReceiver listener)
    {
        if (_events == null) return;

        if (!_events.ContainsKey(eventsType)) return;

        _events[eventsType] -= listener;
    }

    public static void TriggerEvent(EventsType eventsType , params object[] parameters)
    {
        if (_events == null) return;

        if (!_events.ContainsKey(eventsType)) return;

        _events[eventsType](parameters);
    }
}
