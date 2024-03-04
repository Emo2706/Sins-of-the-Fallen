using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManagerSecondZone : MonoBehaviour
{
    public static ManagerSecondZone instance;
    [SerializeField] int _amountSecondZone;
    public static event Action OpenSecondZone = delegate { };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }


    }


    public void Kill()
    {
        _amountSecondZone--;

        if (_amountSecondZone <= 0)
        {
            OpenSecondZone();
            Destroy(gameObject);
            Debug.Log("Segunda zona terminada");
        }

    }
    
}
