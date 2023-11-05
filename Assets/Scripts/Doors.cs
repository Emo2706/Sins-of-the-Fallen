using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        ManagerFirstZone.OpenFirstZone += TheOpenDoor;
    }

    

    public void TheOpenDoor()
    {
        //Sweet Sacrifice
    }
}
