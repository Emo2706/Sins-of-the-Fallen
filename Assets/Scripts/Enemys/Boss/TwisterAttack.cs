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
        _mat = GetComponent<Renderer>().material;
        _matUp = circleUp.GetComponent<Renderer>().material;
        _matDown = circleDown.GetComponent<Renderer>().material;
        _matEdge = circleEdge.GetComponent<Renderer>().material;
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
        
        _matSeq.Append(_matUp.DOFloat(2, _frozenLevel, _transitionDuration));
        _matSeq.Append(_mat.DOFloat(2, _frozenLevel, _transitionDuration));
        _matSeq.Append(_matDown.DOFloat(2, _frozenLevel, _transitionDuration));
        _matSeq.Append(_matEdge.DOFloat(2, _frozenLevel, _transitionDuration));
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

        _speed = initialSpeed;
    }
}
