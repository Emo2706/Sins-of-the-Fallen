using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
   [SerializeField] Animator _anim;

    private void Start()
    {
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_BossDefeated, TheOpenDoor);
    }

    public void TheOpenDoor(params object[] parameters)
    {
        _anim.SetTrigger("TheOpenDoor");
        AudioManager.instance.Play(AudioManager.Sounds.TheOpenDoor);
    }
}
