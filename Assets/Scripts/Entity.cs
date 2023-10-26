using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected int _maxLife;
    [SerializeField] protected int _life;

    public virtual void TakeDmg(int dmg)
    {
        _life -= dmg;
        
    }

   
}
