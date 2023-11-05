using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Ammo")]
    [SerializeField] protected int _totalAmmo = 240;
    [SerializeField] protected int _ammoPerMag = 30;
    [SerializeField] protected int _currentMag = 30;

    [Header("Stats")]
    [SerializeField] protected float _shootRange = 100f;
    [SerializeField] protected float _shootCooldown = .25f;
    [SerializeField] protected float _reloadCooldown = 3f;
    [SerializeField] protected int _dmg = 5;

    protected Transform _barrelTransform;
    protected bool _canShoot, _isReloading;
    public bool CanShoot { get { return _canShoot; } }
    public bool IsReloading { get { return _isReloading; } }

    protected Ray _shootRay;
    protected RaycastHit _shootRayHit;
    protected LayerMask _shootMask;

    public void SetParams(Transform barrelTransform, LayerMask shootMask)
    {
        _barrelTransform = barrelTransform;
        _shootMask = shootMask;

        _canShoot = true;
    }

    abstract protected void ShotBehaviour();

    virtual public void Shoot()
    {
        if(_currentMag > 0)
        {
            _currentMag--;

            print($"<color=yellow>{_currentMag}</color>/<color=orange>{_ammoPerMag}</color>");

            ShotBehaviour();

            StartCoroutine(ShotCooldown());
        }
        else
        {
            Reload();
        }
    }

    public virtual void Reload()
    {
        if(_totalAmmo > 0 && _currentMag < _ammoPerMag)
        {
            int neededAmmo = _ammoPerMag - _currentMag;

            if(neededAmmo <= _totalAmmo)
            {
                _totalAmmo -= neededAmmo;
                _currentMag = _ammoPerMag;
            }
            else
            {
                _currentMag += _totalAmmo;
                _totalAmmo = 0;
            }

            StartCoroutine(ReloadCooldown());
        }
    }

    private IEnumerator ShotCooldown()
    {
        _canShoot = !_canShoot;

        yield return new WaitForSeconds(_shootCooldown);

        _canShoot = !_canShoot;
    }

    private IEnumerator ReloadCooldown()
    {
        print($"<color=gray>Reloading!</color>");

        _isReloading = !_isReloading;
        _canShoot = !_canShoot;

        yield return new WaitForSeconds(_reloadCooldown);

        _isReloading = !_isReloading;
        _canShoot = !_canShoot;

        print($"<color=green>Mag ready!</color>");
    }
}
