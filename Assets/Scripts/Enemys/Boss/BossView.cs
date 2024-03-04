using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossView
{
    Animator _anim;

    Boss _boss;

    public BossView(Boss boss)
    {
        _anim = boss.GetComponentInChildren<Animator>();

        _boss = boss;
    }
    public void Zone()
    {
        if (_boss.life > 0)
            _anim.SetTrigger("Zone");
    }

    public void Dead()
    {
        _anim.SetTrigger("Die");
    }
}
