using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    public Image bar;
    [SerializeField] Boss _boss;
    float fillAmount;

    

    void Start()
    {
        _boss = FindObjectOfType<Boss>();
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_BossDefeated, DeactivateUI);
        Songs.OnEnterBossZone += ActivateUI;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (_boss != null)
        {
            //fillAmount = _boss.life/
            bar.fillAmount = _boss.life/ 150f ;

        }

        else
            _boss = FindObjectOfType<Boss>();

    }

    public void ActivateUI()
    {
        gameObject.SetActive(true);
    }

    public void DeactivateUI(params object[] parameters)
    {
        gameObject.SetActive(false);
    }
}
