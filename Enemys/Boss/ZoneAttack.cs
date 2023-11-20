using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAttack : MonoBehaviour
{
    [SerializeField] int _lifeTime;
    float _lifeTimer;
    public Collider zoneCollider;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.pause) return;

        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _lifeTime)
        {
            ZoneAttackFactory.instance.ReturnToPool(this);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            zoneCollider.enabled=false;
        }
    }



    private void Reset()
    {
        _lifeTimer = 0;
        zoneCollider.enabled = true;
    }

    public static void TurnOnCallBack(ZoneAttack zone)
    {
        zone.Reset();
        zone.gameObject.SetActive(true);
    }


    public static void TurnOffCallBack(ZoneAttack zone)
    {
        zone.gameObject.SetActive(false);
    }

}
