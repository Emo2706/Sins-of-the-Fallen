using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI
{
    Player _player;
    Image _hpBar;
    float _fillAmountImage;
    SliderUI _sliderUI;

    public Dictionary<bulletType, List<GameObject>> _UIElements = new Dictionary<bulletType, List<GameObject>>();

    public Player_UI(Image hpBar , Player player, SliderUI sliderUI)
    {
        _hpBar = hpBar;
        _player = player;
        _UIElements.Add(bulletType.Fireball, player.FireUI);
        _UIElements.Add(bulletType.Iceball, player.IceUI);
        _sliderUI = sliderUI;
    }

    public void Update()
    {
        _fillAmountImage = _player.life / 50f;
        _hpBar.fillAmount = _fillAmountImage;
        
    }

    public void ActivateUI(bulletType type)
    {
        foreach (var item in _UIElements[type])
        {
            item.SetActive(true);
        }
        _sliderUI.ChangeSlider(type);
 
    }

    public void DeactivateUI(bulletType type)
    {
        foreach (var item in _UIElements[type])
        {
            item.SetActive(false);
        }

    }


}
