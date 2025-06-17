using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected int _maxLife;
    public int life;
    //private bool _isFrozen = false; 
    //public int life { get { return _life; } set { _life = value; }}

    public virtual void TakeDmg(int dmg)
    {
        life -= dmg;
        
    }

    /*private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Slowprojectile")
        {
            if(!_isFrozen)
            {
                StartCoroutine(SlowEnemy());
            }

        }


    }

    private IEnumerator SlowEnemy()
    {
        _isFrozen = true;
        Debug.Log("Congelado");
        Speed /= 2;

        yield return new WaitForSeconds(3.2f);
        Speed *= 2;
        _isFrozen = false;

    }*/




}
