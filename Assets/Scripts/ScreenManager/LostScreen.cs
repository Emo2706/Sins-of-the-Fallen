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

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_PlayerDead, ActivateScreen);

    }

    private void Start()
    {
        gameObject.SetActive(false);

    }

    void ActivateButtons(bool enable)
    {
        foreach (var button in _buttons)
        {
            button.interactable = enable;
        }

        Cursor.visible = enable;

        if (enable == true) Cursor.lockState = CursorLockMode.Confined;
    }

    public void ActivateScreen(params object[] parameters)
    {
        gameObject.SetActive(true);
        ScreenManager.instance.Push("LostScreen");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Debug.Log("Activate");
        ActivateButtons(true);
    }


    public void Activate()
    {

    }

    public void Deactivate()
    {
        ActivateButtons(false);
    }

    public void Free()
    {
        Destroy(gameObject);
    }

    public void Restart()
    {
        LevelManager.instance.RestartLevel();
    }

    public void ExitToMenu()
    {
        LevelManager.instance.StartLevel(0);
    } 

}
