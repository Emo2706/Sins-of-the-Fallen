using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBall : MonoBehaviour
{
    public Slider timeSlider;
    [SerializeField] float _scaleMultiplier;
    Vector3 addScale;
    [SerializeField] int _explodeTimer;
    [SerializeField] int _cooldownLost;
    public Image hpBar;
    float _fillAmountImage;
    [SerializeField] int _hp;
    [SerializeField] float _maxScale;
    //[SerializeField] ParticleSystem _lightning;

    private void Start()
    {
        addScale = Vector3.zero;
        timeSlider.value = 0f;
        timeSlider.gameObject.SetActive(true);
        hpBar.gameObject.SetActive(true);
    }

    void Update()
    {
        addScale.x += Time.deltaTime * _scaleMultiplier;
        addScale.y += Time.deltaTime * _scaleMultiplier;
        addScale.z += Time.deltaTime * _scaleMultiplier;

        transform.localScale += addScale;

        Vector3 clampedScale = transform.localScale;

        clampedScale.x = Mathf.Min(clampedScale.x, _maxScale);
        clampedScale.y = Mathf.Min(clampedScale.y, _maxScale);
        clampedScale.z = Mathf.Min(clampedScale.z, _maxScale);

        transform.localScale = clampedScale;

        timeSlider.value += Time.deltaTime;

        //if(timeSlider.value >= _explodeTimer/2) _lightning.emission.bu

        if (timeSlider.value >= _explodeTimer) ScreenManager.instance.Push("LostScreen");
    }

    IEnumerator LostEvent(int cooldownLost)
    {
        //Efecto explosion bola

        yield return new WaitForSeconds(cooldownLost);

        ScreenManager.instance.Push("LostScreen");
    }

    public static void TurnOnCallBack(BossBall ball)
    {
        ball.gameObject.SetActive(true);
    }

    public static void TurnOffCallBack(BossBall ball)
    {
        ball.gameObject.SetActive(false);
    }

    public void TakeDmg(int dmg)
    {
        _hp -= dmg;

        _fillAmountImage = _hp / 40f;
        hpBar.fillAmount = _fillAmountImage;

        if (_hp <= 0)
        {
            ScreenManager.instance.Push("WinScreen");
        }
    }
}
