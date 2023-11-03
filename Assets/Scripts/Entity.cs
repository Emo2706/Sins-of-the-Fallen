using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected int _maxLife;
    public int life;
    //public int life { get { return _life; } set { _life = value; }}

    public virtual void TakeDmg(int dmg)
    {
        life -= dmg;
        
    }



   
}
