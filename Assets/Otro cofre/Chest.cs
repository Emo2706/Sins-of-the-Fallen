using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] ParticleSystem _sys1, _sys2;
    bool _wasOpened = false;
    [SerializeField] TMP_Text _text;

    void Start()
    {
        CurBehaviour = delegate { };
        mat_Chest = chestMesh.materials[1];
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(transform.position, player.gameObject.transform.position);

        if(distance <= interactionRangeDetect && _wasOpened == false)
        {
            _text.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
                OpenChest();
        }

        else _text.gameObject.SetActive(false);


        CurBehaviour();
    }
    

    void OpenChest()
    {
        _animator.SetBool("Open", true);
        _soulparticle.Play();
        _wasOpened = true;
        CurBehaviour = ShaderTransition;
        _sys1.Stop();
        _sys2.Stop();
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
