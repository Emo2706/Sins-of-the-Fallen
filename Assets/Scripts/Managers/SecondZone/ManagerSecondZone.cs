using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManagerSecondZone : MonoBehaviour
{
    public static ManagerSecondZone instance;
    [SerializeField] int _amountSecondZone;
    [SerializeField] int _amountLeftZone;
    public static event Action OpenSecondZone = delegate { };
    public static event Action OpenLeftZone = delegate{};

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

    public void KillLeft()
    {
        _amountLeftZone--;

        if (_amountLeftZone <= 0)
        {
            OpenLeftZone();
            Destroy(gameObject);
            Debug.Log("Segunda zona terminada");
        }
    }
    
}
