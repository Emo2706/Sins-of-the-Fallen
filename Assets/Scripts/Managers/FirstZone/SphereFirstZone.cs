using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereFirstZone : MonoBehaviour
{
    [SerializeField] MeshRenderer _meshRenderer; 

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        ManagerFirstZone.OpenFirstZone += Desactivate;
        ColliderFirstZone.FirstZone += Activate;
    }

    public void Activate()
    {
        _meshRenderer.enabled = true;
    }

    public void Desactivate()
    {
        gameObject.SetActive(false);
    }
}
