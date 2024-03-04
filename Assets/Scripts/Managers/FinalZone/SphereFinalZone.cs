using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereFinalZone : MonoBehaviour
{
    [SerializeField] MeshRenderer _meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();


        Songs.OnEnterBossZone += Activate;

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_BossDefeated, Deactivate);
    }

    public void Activate()
    {
        _meshRenderer.enabled = true;
    }

    public void Deactivate(params object[] parameters)
    {
        gameObject.SetActive(false);
    }
}
