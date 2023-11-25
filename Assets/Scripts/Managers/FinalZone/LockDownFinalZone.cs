using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDownFinalZone : MonoBehaviour
{
   [SerializeField] MeshRenderer _mr;
   [SerializeField] Collider _collider;

    private void Start()
    {
        Songs.OnEnterBossZone += OnZone;

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_BossDefeated, OnBossDeath);
    }

    public void OnZone()
    {
        _mr.enabled = true;
        _collider.enabled = true;
    }


    public void OnBossDeath(params object[] parameters)
    {
        gameObject.SetActive(false);
    }
    
}
