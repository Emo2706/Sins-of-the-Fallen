using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsShield : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            TargetsShieldFactory.instance.ReturnToPool(this);
            TargetsShieldFactory.instance.initialAmount--;
        }
    }

    private void Reset()
    {
        
    }

    public static void TurnOffCallBack(TargetsShield targetShield)
    {
        targetShield.Reset();
        targetShield.gameObject.SetActive(false);
    }


    public static void TurnOnCallBack(TargetsShield targetShield)
    {
        targetShield.gameObject.SetActive(true);
    }

}
