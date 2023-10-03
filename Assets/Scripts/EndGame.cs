using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_PlayerDead , GameOver);
    }

    void GameOver(params object[] p)
    {
        gameObject.SetActive(true);

        EventManager.UnSubscribeToEvent(EventManager.EventsType.Event_PlayerDead, GameOver);
    }
}
