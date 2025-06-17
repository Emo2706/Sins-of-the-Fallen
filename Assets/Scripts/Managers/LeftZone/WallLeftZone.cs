using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallLeftZone : MonoBehaviour
{
    [SerializeField] Collider _collider;

    // Start is called before the first frame update
    void Start()
    {
        ManagerSecondZone.OpenLeftZone += Desactivate;
        LeftZone.LeftZoneEvent += ActivateCollider;
    }

    public void Desactivate()
    {
        gameObject.SetActive(false);
    }

    public void ActivateCollider()
    {
        _collider.enabled = true;
    }

    private void OnDestroy()
    {
        ManagerSecondZone.OpenLeftZone -= Desactivate;
        LeftZone.LeftZoneEvent -= ActivateCollider;
    }
}
