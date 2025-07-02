using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    public Image bar;
    float fillAmount;
    public Boss boss;

    void Update()
    {
         bar.fillAmount = boss.life/ 150f ;
    }
}
