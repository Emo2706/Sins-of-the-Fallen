using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance;

    Stack<IScreen> _screenStack;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else Destroy(gameObject);


        _screenStack = new Stack<IScreen>();
    }

    public void Push(IScreen newScreen)
    {
        if (_screenStack.Count > 0)
            _screenStack.Peek().Deactivate();

        _screenStack.Push(newScreen);

        newScreen.Activate();
    }

    public void Push(string resourceName)
    {
        var gmObj = Instantiate(Resources.Load<GameObject>(resourceName));

        if (gmObj.TryGetComponent(out IScreen newScreen))
            Push(newScreen);
    }

    public void Pop()
    {
        if (_screenStack.Count <= 1) return;

        _screenStack.Pop().Free();

        if (_screenStack.Count == 0) return;

        _screenStack.Peek().Activate();
    }
}
