using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseInput : CommandInputs
{
    PauseScreen _pauseScreen;

    public PauseInput(PauseScreen pauseScreen)
    {
        _pauseScreen = pauseScreen;
    }


    public override void Execute()
    {
        ScreenManager.instance.Push(_pauseScreen);
    }
}
