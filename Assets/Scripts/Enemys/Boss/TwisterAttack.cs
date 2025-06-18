using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TwisterAttack : MonoBehaviour
{
    float _lifeTimer;
    [SerializeField] int _lifeTime;
    [SerializeField] float _transitionDuration;
    [SerializeField] GameObject circleUp;
    [SerializeField] GameObject circleDown;
    Sequence _matSeq;
    Material _mat , _matUp , _matDown;
    [SerializeField] Collider _collider;
    [SerializeField] Collider _freezeCollider;
    int _frozenLevel = Shader.PropertyToID("_FrozenLevel");

    private void Start()
    {
        _mat = GetComponent<Renderer>().material;
        _matUp = circleUp.GetComponent<Renderer>().material;
        _matDown = circleDown.GetComponent<Renderer>().material;
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

    public void TornadoFreeze()
    {
        _matSeq.Kill();

        _matSeq.Append(_matUp.DOFloat(2, _frozenLevel, _transitionDuration));
        _matSeq.Append(_mat.DOFloat(2, _frozenLevel, _transitionDuration));
        _matSeq.Append(_matDown.DOFloat(2, _frozenLevel, _transitionDuration));

        _collider.enabled = false;
        _freezeCollider.enabled = true;
        Debug.Log("Freeze");
    }
}
