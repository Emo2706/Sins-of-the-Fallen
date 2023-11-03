using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour,IScreen
{
    Button[] _buttons;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();

        ActivateButtons(false);
    }

    void ActivateButtons(bool enable)
    {
        foreach (var button in _buttons)
        {
            button.interactable = enable;
        }
    }

    public void BTN_Options()
    {
        //ScreenManager.instance.Push("Canvas Options");
    }

    public void BTN_Back()
    {
        ScreenManager.instance.Pop();
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


