using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawnEnemys : MonoBehaviour
{
    float _lifeTimer;
    [SerializeField] float _cooldown;

    

    // Update is called once per frame
    void Update()
    {
        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _cooldown)
            gameObject.SetActive(false);
    }
}
