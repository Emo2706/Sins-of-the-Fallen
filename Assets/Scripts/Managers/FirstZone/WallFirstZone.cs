using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFirstZone : MonoBehaviour
{
    [SerializeField] Collider _collider;

    // Start is called before the first frame update
    void Start()
    {

        ManagerFirstZone.OpenFirstZone += Desactivate;
        ColliderFirstZone.FirstZone += ActivateCollider;
    }

   public void Desactivate()
    {
        gameObject.SetActive(false);
    }

    public void ActivateCollider()
    {
        _collider.enabled = true;
    }
}
