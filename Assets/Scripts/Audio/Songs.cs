using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Songs : MonoBehaviour
{
    [SerializeField] int _werewolfDuration = 240;
    [SerializeField] int _eliteDuration = 241;
    [SerializeField] Collider _songsCollider;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            StartCoroutine(SongsCoroutine());
            _songsCollider.enabled = false;
        }
        Debug.Log("Entro , suena uzi");
    }

    

    IEnumerator SongsCoroutine()
    {
        WaitForSeconds werewolf = new WaitForSeconds(_werewolfDuration);
        
        WaitForSeconds elite = new WaitForSeconds(_eliteDuration);

        AudioManager.instance.Play(AudioManager.Sounds.Werewolf);

        yield return werewolf ;

        AudioManager.instance.Stop(AudioManager.Sounds.Werewolf);

        AudioManager.instance.Play(AudioManager.Sounds.Elite);

        yield return elite;

        AudioManager.instance.Stop(AudioManager.Sounds.Elite);

        AudioManager.instance.Play(AudioManager.Sounds.Lotion);
    }
}
