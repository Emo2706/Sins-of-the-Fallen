using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour,IScreen
{
    public void BTN_Back()
    {
        GameManager.instance.pause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        ScreenManager.instance.Pop();
    }

    public void Activate()
    {
        GameManager.instance.pause = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Deactivate()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Free()
    {
        Destroy(gameObject);
    }

    public void Restart()
    {
        ScreenManager.instance.Pop();
        LevelManager.instance.RestartLevel();
        
    }

    public void ExitToMenu()
    {
        LevelManager.instance.StartLevel(0);
        ScreenManager.instance.Pop();
    }
}


