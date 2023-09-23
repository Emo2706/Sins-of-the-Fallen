using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public int maxLife;
    public int life;

    public virtual void TakeDmg(int dmg)
    {
        life = dmg;
    }
}
