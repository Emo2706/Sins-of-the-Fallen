using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LostScreen :MonoBehaviour ,IScreen
{
    Button[] _buttons;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();

        ActivateButtons(false);
        
        EventManager.SubscribeToEvent(EventManager.EventsType.Event_PlayerDead, Activate);
    }

    void ActivateButtons(bool enable)
    {
        foreach (var button in _buttons)
        {
            button.interactable = enable;
        }
    }

    public void Activate(params object[] parameters)
    {
        Activate();
    }


    public void Activate()
    {
        ActivateButtons(true);

    }

    public void Deactivate()
    {
        ActivateButtons(false);
    }

    public void Free()
    {
        Destroy(gameObject);
    }

    
}
