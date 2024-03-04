using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsSecondZone : MonoBehaviour
{
    [SerializeField] Collider _collider;

    // Start is called before the first frame update
    void Start()
    {
        SecondZone.SecondZoneEvent += ActivateCollider;

        ManagerSecondZone.OpenSecondZone += Deactivate;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void ActivateCollider()
    {
        _collider.enabled = true;
    }
    
}
