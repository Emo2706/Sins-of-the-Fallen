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
        AudioManager.instance.SetVolume(AudioManager.Sounds.Korn, 0);
    }

    public void Deactivate()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        AudioManager.instance.SetVolume(AudioManager.Sounds.Korn, 1);
    }

    public void Free()
    {
        Destroy(gameObject);
    }

    public void Restart()
    {
        AudioManager.instance.StopAllsounds();
        ScreenManager.instance.Pop();
        LevelManager.instance.RestartLevel();
        
    }

    public void ExitToMenu()
    {
        AudioManager.instance.StopAllsounds();
        LevelManager.instance.StartLevel(0);
        ScreenManager.instance.Pop();
    }
}


