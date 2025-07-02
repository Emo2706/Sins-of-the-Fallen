using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] BossBar _boss;

    private void Start()
    {
        Songs.OnEnterBossZone += ActivateUI;
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_BossDefeated, DeactivateUI);
    }

    void ActivateUI()
    {
        _boss.gameObject.SetActive(true);
    }

    public void DeactivateUI(params object[] parameters)
    {
        _boss.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventManager.UnSubscribeToEvent(EventManager.EventsType.Event_BossDefeated, DeactivateUI);
        Songs.OnEnterBossZone -= ActivateUI;
    }
}
