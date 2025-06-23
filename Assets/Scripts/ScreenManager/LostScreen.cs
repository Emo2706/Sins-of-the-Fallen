using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LostScreen :MonoBehaviour ,IScreen
{
    public void Activate()
    {
        GameManager.instance.pause = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        AudioManager.instance.StopAllsounds();
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
        LevelManager.instance.RestartLevel();
        ScreenManager.instance.Pop();
    }

    public void ExitToMenu()
    {
        LevelManager.instance.StartLevel(0);
        ScreenManager.instance.Pop();
    }

}
