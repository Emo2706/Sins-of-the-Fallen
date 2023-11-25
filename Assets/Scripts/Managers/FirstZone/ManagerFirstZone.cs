using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ManagerFirstZone : MonoBehaviour
{
    public static ManagerFirstZone instance;
    [SerializeField] int _amountFirstZone;
    public static event Action OpenFirstZone = delegate { };

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
        _amountFirstZone--;

        if (_amountFirstZone <=0)
        {
            OpenFirstZone();
            Destroy(gameObject);
            Debug.Log("Primera zona terminada");
        }

    }
}
