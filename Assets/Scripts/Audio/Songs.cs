using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Songs : MonoBehaviour
{
    [SerializeField] int _werewolfDuration = 240;
    [SerializeField] int _eliteDuration = 241;
    [SerializeField] Collider _songsCollider;

    public static event Action OnEnterBossZone = delegate { };

    private void Start()
    {
        OnEnterBossZone += OnEnterFinalZone;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            OnEnterBossZone();
            _songsCollider.enabled = false;
        }
    }

    public void OnEnterFinalZone()
    {
        StartCoroutine(SongsCoroutine());
    }


    IEnumerator SongsCoroutine()
    {
        WaitForSeconds werewolf = new WaitForSeconds(_werewolfDuration);
        
        WaitForSeconds elite = new WaitForSeconds(_eliteDuration);

        AudioManager.instance.Play(AudioManager.Sounds.BossScream);

        AudioManager.instance.Play(AudioManager.Sounds.Korn);

        AudioManager.instance.Stop(AudioManager.Sounds.Ambience);

        yield return werewolf ;

        AudioManager.instance.Stop(AudioManager.Sounds.Elite);

        AudioManager.instance.Play(AudioManager.Sounds.Lotion);
        
    }

    private void OnDestroy()
    {
        OnEnterBossZone -= OnEnterFinalZone;
    }
}
