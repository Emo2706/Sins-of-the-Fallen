using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] Animator _anim;
    [SerializeField] AudioSource _opening;

    // Start is called before the first frame update
    void Start()
    {
        ManagerFirstZone.OpenFirstZone += TheOpenDoor;
    }

    

    public void TheOpenDoor()
    {
        _anim.SetTrigger("TheOpenDoor");
        _opening.Play();
        //AudioManager.instance.Play(AudioManager.Sounds.TheOpenDoor);
    }

    private void OnDestroy()
    {
        ManagerFirstZone.OpenFirstZone -= TheOpenDoor;
    }
}
