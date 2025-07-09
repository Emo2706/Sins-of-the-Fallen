using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TwisterAttack : MonoBehaviour
{
    float _lifeTimer;
    [SerializeField] int _lifeTime;
    [SerializeField] float _transitionDurationIceToFire;
    [SerializeField] float _transitionDurationFireToIce;
    [SerializeField] GameObject circleUp;
    [SerializeField] GameObject circleDown;
    [SerializeField] GameObject circleEdge;
    [SerializeField] int _speed;
    [SerializeField] List<Transform> _wayPoints;
    Sequence _matSeq;
    Material _mat , _matUp , _matDown, _matEdge;
    [SerializeField] Collider _collider;
    [SerializeField] Collider _freezeCollider;
    [SerializeField] bool _move;
    [SerializeField] float _distance;
    [SerializeField] float _freezeDuration;
    int _indexWayPoint;
    int _frozenLevel = Shader.PropertyToID("_FrozenLevel");
    Vector3 _dir;
    [SerializeField] AudioSource _tornado;
    [SerializeField] AudioSource _freeze;

    private void Start()
    {
        _mat = new Material(GetComponent<Renderer>().sharedMaterial);
        GetComponent<Renderer>().material = _mat;

        /*_mat = GetComponent<Renderer>().material;
        _matUp = circleUp.GetComponent<Renderer>().material;
        _matDown = circleDown.GetComponent<Renderer>().material;
        _matEdge = circleEdge.GetComponent<Renderer>().material;*/

        _matUp = _mat;
        _matDown = _mat;
        _matEdge = _mat;

        circleUp.GetComponent<Renderer>().material = _mat;
        circleDown.GetComponent<Renderer>().material = _mat;
        circleEdge.GetComponent<Renderer>().material = _mat;
        //AudioManager.instance.Play(AudioManager.Sounds.Tornado);
        _tornado.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.pause) return;

        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _lifeTime)
            TwisterAttackFactory.instance.ReturnToPool(this);

        if (_move) Move();
    }

    private void FixedUpdate()
    {
        if (_move) transform.position = Vector3.MoveTowards(transform.position, _dir, _speed * Time.fixedDeltaTime);
    }

    void Move()
    {
        _dir = _wayPoints[_indexWayPoint].position;

        var distance = _dir - transform.position;

        if(distance.sqrMagnitude<= _distance * _distance)
        {
            _indexWayPoint++;

            if (_indexWayPoint >= _wayPoints.Count) _indexWayPoint = 0;
        }
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

        _matSeq = DOTween.Sequence();

      //_matSeq.Append(_matDown.DOFloat(4, _frozenLevel, _transitionDurationFireToIce));
        _matSeq.Append(_mat.DOFloat(4, _frozenLevel, _transitionDurationFireToIce));
      //_matSeq.Append(_matUp.DOFloat(4, _frozenLevel, _transitionDurationFireToIce));
      //_matSeq.Append(_matEdge.DOFloat(4, _frozenLevel, _transitionDurationFireToIce));
       
        //AudioManager.instance.Play(AudioManager.Sounds.Freeze);
        //AudioManager.instance.Stop(AudioManager.Sounds.Tornado);
        _freeze.Play();
        _tornado.Stop();
        _collider.enabled = false;
        _freezeCollider.enabled = true;
        StartCoroutine(LowSpeed(_freezeDuration));
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateCollider());
    }

    IEnumerator DeactivateCollider()
    {
        _collider.enabled = false;

        yield return new WaitForSeconds(1f);

        _collider.enabled = true;
    }

    IEnumerator LowSpeed(float duration)
    {
        var initialSpeed = _speed;

        _speed = 0;

        yield return new WaitForSeconds(duration);

        _matSeq.Kill();

        _matSeq = DOTween.Sequence();

      //_matSeq.Append(_matEdge.DOFloat(0, _frozenLevel, _transitionDurationIceToFire));
      //_matSeq.Append(_matUp.DOFloat(0, _frozenLevel, _transitionDurationIceToFire));
        _matSeq.Append(_mat.DOFloat(0, _frozenLevel, _transitionDurationIceToFire));
      //_matSeq.Append(_matDown.DOFloat(0, _frozenLevel, _transitionDurationIceToFire));
        

        _speed = initialSpeed;

        _collider.enabled = true;
        _freezeCollider.enabled = false;
    }
}
