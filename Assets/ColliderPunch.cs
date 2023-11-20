using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPunch : MonoBehaviour
{
    [SerializeField] Collider _colliderPunch;

    public void ActivateCollider()
    {
        _colliderPunch.gameObject.SetActive(true);
    }
    
    public void DesactivateCollider()
    {
        _colliderPunch.gameObject.SetActive(false);
    }
}
