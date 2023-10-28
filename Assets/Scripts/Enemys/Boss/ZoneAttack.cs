using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAttack : MonoBehaviour
{
    [SerializeField] int _lifeTime;
    float _lifeTimer;
    public Collider _zoneCollider;

    // Update is called once per frame
    void Update()
    {
        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _lifeTime)
        {
            ZoneAttackFactory.instance.ReturnToPool(this);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
            _zoneCollider.enabled=false;
    }



    private void Reset()
    {
        _lifeTimer = 0;
        _zoneCollider.enabled = true;
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
