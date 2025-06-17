using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerScreens : MonoBehaviour
{
    [SerializeField] Transform _mainGame;
    // Start is called before the first frame update
    void Start()
    {
        ScreenManager.instance.Push(new ScreenGameplay (_mainGame));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ScreenManager.instance._screenStack.Count<2)
            {
                 ScreenManager.instance.Push("PauseScreen");
                 GameManager.instance.pause = true;
            }
        }

    
    }
}
