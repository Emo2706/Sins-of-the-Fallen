using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereLeftZone : MonoBehaviour
{
    [SerializeField] MeshRenderer _meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        LeftZone.LeftZoneEvent += Activate;
        ManagerSecondZone.OpenLeftZone += Deactivate;
    }

    public void Activate()
    {
        _meshRenderer.enabled = true;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        LeftZone.LeftZoneEvent -= Activate;
        ManagerSecondZone.OpenLeftZone -= Deactivate;
    }
}
