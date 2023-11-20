using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WinScreen : MonoBehaviour, IScreen
{
    Button[] _buttons;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_WinGame, ActivateScreen);
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
        
        Debug.Log("ActivateButtons");
    }

    public void ActivateScreen(params object[] parameters)
    {
        gameObject.SetActive(true);
        ScreenManager.instance.Push("WinScreen");
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
