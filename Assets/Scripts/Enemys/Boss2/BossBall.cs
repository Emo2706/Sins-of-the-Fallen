using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBall : MonoBehaviour
{
    public Slider timeSlider;
    [SerializeField] int _scaleMultiplier;
    Vector3 addScale;
    [SerializeField] int _cooldown;
    [SerializeField] int _cooldownLost;
    [SerializeField] Slider _hpBar;
    [SerializeField] int _hp;

    private void Start()
    {
        addScale = Vector3.zero;
        timeSlider.value = 0f;
        timeSlider.gameObject.SetActive(true);
    }

    void Update()
    {
        addScale.x += Time.deltaTime * _scaleMultiplier;
        addScale.y += Time.deltaTime * _scaleMultiplier;
        addScale.z += Time.deltaTime * _scaleMultiplier;

        transform.localScale += addScale;

        timeSlider.value += Time.deltaTime;

        if (timeSlider.value >= _cooldown) StartCoroutine(LostEvent(_cooldownLost));
    }

    IEnumerator LostEvent(int cooldownLost)
    {
        //Efecto explosion bola

        yield return new WaitForSeconds(cooldownLost);

        EventManager.TriggerEvent(EventManager.EventsType.Event_PlayerDead);
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

        _hpBar.value = _hp;

        if (_hp <= 0)
        {
            //Efectos ganar etc.
        }
    }
}
