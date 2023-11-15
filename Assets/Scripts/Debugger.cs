using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] Transform _mainGame;
    [SerializeField] PauseScreen _pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        ScreenManager.instance.Push(new ScreenGameplay (_mainGame));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_pauseScreen == null)
            {
                 _pauseScreen = Instantiate(Resources.Load<PauseScreen>("PauseScreen"));
                 ScreenManager.instance.Push(_pauseScreen);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ScreenManager.instance.Push("LostScreen");
        }
    }
}
