using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float interactionRangeDetect;
    [SerializeField] Animator _animator;
    [SerializeField] ParticleSystem _soulparticle;
    [SerializeField] SkinnedMeshRenderer chestMesh;

    [SerializeField] Player player;
    Material mat_Chest;
    float _chargeCounter;
    Action CurBehaviour;
    void Start()
    {
        CurBehaviour = delegate { };
        mat_Chest = chestMesh.materials[1];
    }

    // Update is called once per frame
    void Update()
    {
        //Poner en el if, un && Vector3.distance(transform.position, player.transform.positon) <= interactionRangeDetect
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, player.gameObject.transform.position) <= interactionRangeDetect)
        {
            OpenChest();
        }
        CurBehaviour();
    }
    

    void OpenChest()
    {
        _animator.SetBool("Open", true);
        _soulparticle.Play();
        CurBehaviour = ShaderTransition;
    }

    void ShaderTransition()
    {
        _chargeCounter += Time.deltaTime / 1.5f;
        mat_Chest.SetFloat("_Porcentaje", _chargeCounter);
        if (_chargeCounter >= 1)
        {
            _chargeCounter = 1;
            CurBehaviour = delegate { };
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRangeDetect);
    }


}
