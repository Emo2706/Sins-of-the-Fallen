using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderUI : MonoBehaviour
{

    [SerializeField] GameObject[] _markers;
    [SerializeField] float[] _phasesInSeconds;
     float maximumCharge;
    [SerializeField] float chargeCounter;
    [SerializeField] Slider slider;
    [SerializeField] Animator _anim;
   
    int _actualPhase;
    [SerializeField] TMP_Text _SliderText;
     Material _materialBG;
    [SerializeField] Image _bg;
    Player _player;
   

    public Dictionary<bulletType, Slider> ElementalSliders = new Dictionary<bulletType, Slider>();
    public Dictionary<bulletType, TMP_Text> ElementalTexts = new Dictionary<bulletType, TMP_Text>();

    public SliderUI(Player player)
    {
        _player = player;
        _phasesInSeconds = player.phasesCooldowns;
        slider = player.sliderFireBall;
        _SliderText = player.fireTxt;
        _anim = player.anim;
        _bg = player.imgFire;
        _materialBG = player.matFire;
        _markers = player.fireMarkers;
        ElementalSliders.Add(bulletType.Fireball, player.sliderFireBall);
        ElementalSliders.Add(bulletType.Iceball, player.sliderIceBall);
        ElementalTexts.Add(bulletType.Fireball, player.fireTxt);
        ElementalTexts.Add(bulletType.Iceball, player.iceTxt);
    }

    // Start is called before the first frame update
   public void Start()
    {
        slider.maxValue = _phasesInSeconds[_phasesInSeconds.Length - 1];
        maximumCharge = _phasesInSeconds[_phasesInSeconds.Length - 1];
        _materialBG = _bg.material;
        CalculateMarkersPos();
    }

    // Update is called once per frame
   public void Update()
    {

        

        if (chargeCounter >= _phasesInSeconds[_actualPhase])
        {
            CheckReachedCharge();
            
        }

    
    }

    void AltStart()
    {
        slider.maxValue = _phasesInSeconds[_phasesInSeconds.Length - 1];
        maximumCharge = _phasesInSeconds[_phasesInSeconds.Length - 1];
        _materialBG = _bg.material;
        CalculateMarkersPos();
    }

    public void ChangeSlider(bulletType type)
    {
        slider = ElementalSliders[type];
        _SliderText = ElementalTexts[type];
        if(type == bulletType.Fireball)
        {
            _markers = _player.fireMarkers;
        }
        else
        {
            _markers = _player.iceMarkers;
        }
        AltStart();
    }


    void CalculateMarkersPos()
    {
        for (int i = 0; i < _markers.Length; i++)
        {
            //_markers[i].transform.Rotate(Vector3.zero);
            float rotation = _phasesInSeconds[i] * 360 / maximumCharge * -1;
            _SliderText.text = _actualPhase.ToString();

            _markers[i].transform.localRotation = Quaternion.Euler(0, 0, rotation);
        }
        
    }
    public void FillCharge()
    {
        chargeCounter = Mathf.Clamp(chargeCounter + Time.deltaTime, 0, maximumCharge);
        _materialBG.SetFloat("_Porcentaje", chargeCounter / maximumCharge);
        slider.value = chargeCounter;
    }

    public void ResetSlidedrState()
    {
        chargeCounter = 0;
        _actualPhase = 0;
        _SliderText.text = "0";
        _player.StartCoroutine(ResetFill());
        
    }

    IEnumerator ResetFill()
    {
        WaitForSeconds wait = new WaitForSeconds(0.02f);
        float speed = slider.value;
        while (slider.value > 0)
        {
            slider.value -= 50 * speed * Time.deltaTime;
            yield return wait;
        }
    }

    void CheckReachedCharge()
    {
        _actualPhase = Mathf.Clamp(_actualPhase + 1, 0, _phasesInSeconds.Length - 1);


        if (chargeCounter >= _phasesInSeconds[_phasesInSeconds.Length - 1])
        {
            _SliderText.text = _phasesInSeconds.Length.ToString();
        }
        else _SliderText.text = _actualPhase.ToString();

        _anim.SetTrigger("CambioDeNum");

        /*if (_actualPhase == 1)
            AudioManager.instance.Play(AudioManager.Sounds.Charge1);

        if (_actualPhase == 2)
            AudioManager.instance.Play(AudioManager.Sounds.Charge2);*/



        
    }

    IEnumerator FillAnimtext()
    {
        _anim.SetTrigger("CambioDeNum");
        yield return null;
        //var WaitForNextchange = new WaitForSeconds(time);
        /*if (canChangeAnim)
        {
            
            canChangeAnim = false;
        }
       // yield return WaitForNextchange;

        canChangeAnim = true;*/
    }
}


