using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSecondZone : MonoBehaviour
{
    [SerializeField] MeshRenderer _meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();


        SecondZone.SecondZoneEvent += Activate;
        ManagerSecondZone.OpenSecondZone += Deactivate;
    }

    public void Activate()
    {
        _meshRenderer.enabled = true;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
