using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play(AudioManager.Sounds.MusicMenu);
    }

    public void Play()
    {
        LevelManager.instance.StartLevel(1);
        AudioManager.instance.Stop(AudioManager.Sounds.MusicMenu);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
