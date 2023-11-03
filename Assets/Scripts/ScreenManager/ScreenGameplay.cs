using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenGameplay : IScreen
{
    Dictionary<Behaviour, bool> _screenDictionary;

    Transform _root;

    public ScreenGameplay(Transform root)
    {
        _root = root;

        _screenDictionary = new Dictionary<Behaviour, bool>();
    }

    public void Activate()
    {
        foreach (var pair in _screenDictionary)
        {
            pair.Key.enabled = pair.Value;

        }
    }

    public void Deactivate()
    {
        foreach (var pair in _root.GetComponentsInChildren<Behaviour>())
        {
            _screenDictionary[pair] = pair.enabled;

            pair.enabled = false;
        }
    }

    public void Free()
    {
        GameObject.Destroy(_root.gameObject);
    }

  

   
}
