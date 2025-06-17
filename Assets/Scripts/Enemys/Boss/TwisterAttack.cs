using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TwisterAttack : MonoBehaviour
{
    float _lifeTimer;
    [SerializeField] int _lifeTime;
    [SerializeField] float _transitionDuration;
    Sequence _matSeq;
    Material _mat;

    private void Start()
    {
        _mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.pause) return;

        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _lifeTime)
            TwisterAttackFactory.instance.ReturnToPool(this);
    }


    private void Reset()
    {
        _lifeTimer = 0;
    }

    public static void TurnOnCallBack(TwisterAttack attack)
    {
        attack.Reset();
        attack.gameObject.SetActive(true);
    }


    public static void TurnOffCallBack(TwisterAttack attack)
    {
        attack.gameObject.SetActive(false);
    }

    public void Freeze()
    {
        _matSeq.Kill();

        //_matSeq.Append(_mat.DOFloat(1, "ingresar nombre de la propiedad", _transitionDuration));
    }
}
