using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAttackWarning : MonoBehaviour
{
    [SerializeField] int _lifeTime;
    float _lifeTimer;

    private void Start()
    {
        StartCoroutine(Effect());
    }

    // Update is called once per frame
    void Update()
    {
        _lifeTimer += Time.deltaTime;

        if (_lifeTimer>=_lifeTime)
        {
            ZoneAttackWarningFactory.instance.ReturnToPool(this);
        }
    }
    private void Reset()
    {
        _lifeTimer = 0;
    }

    public static void TurnOnCallBack(ZoneAttackWarning zoneWarning)
    {
        zoneWarning.Reset();
        zoneWarning.gameObject.SetActive(true);
    }

    public static void TurnOffCallBack(ZoneAttackWarning zoneWarning)
    {
        zoneWarning.gameObject.SetActive(false);
    }

    IEnumerator Effect()
    {
        WaitForSeconds wait = new WaitForSeconds(0.4f);

        while (true)
        {
            gameObject.SetActive(false);

            yield return wait;

            gameObject.SetActive(true);
        }
    }
}
