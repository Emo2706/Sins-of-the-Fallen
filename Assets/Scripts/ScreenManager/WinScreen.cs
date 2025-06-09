using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WinScreen : MonoBehaviour, IScreen
{
    public void Activate()
    {
        GameManager.instance.pause = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Deactivate()
    {
        GameManager.instance.pause = false;
    }

    public void Free()
    {
        Destroy(gameObject);
    }

    public void Restart()
    {
        ScreenManager.instance.Pop();
        LevelManager.instance.RestartLevel();
        AudioManager.instance.StopAllsounds();
    }

    public void ExitToMenu()
    {
        LevelManager.instance.StartLevel(0);
        ScreenManager.instance.Pop();
    }
}
