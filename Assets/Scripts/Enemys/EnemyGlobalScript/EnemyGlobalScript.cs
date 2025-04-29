using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyGlobalScript : Entity
{
    public Rigidbody _rb;
    public int bulletsDmg;
    public float dieAnimationDuration;
    public List<GameObject> _buffParticles;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        life = _maxLife;
    }

    public void ApplyBurn(int DamagePerTick, float duration, float interval)
    {
        StartCoroutine(BurnCourutine(DamagePerTick, duration, interval));
    }

    private IEnumerator BurnCourutine(int damage, float duration, float interval)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            TakeDmg(damage);
            yield return new WaitForSeconds(interval);
            elapsed += interval;
        }
    }




    public void DisableOnDead()
    {
        enabled = false;
    }

   

    
}
