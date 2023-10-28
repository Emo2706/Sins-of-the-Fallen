using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] Transform _mainGame;

    // Start is called before the first frame update
    void Start()
    {
        ScreenManager.instance.Push(new ScreenGameplay (_mainGame));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }
}
