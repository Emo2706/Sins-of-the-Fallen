using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI
{
    Player _player;
    Slider _healthSlider;

    public Player_UI(Slider healthSlider , Player player)
    {
        _healthSlider = healthSlider;
        _player = player;
    }

    public void Update()
    {
        _healthSlider.value = _player.life;
    }

    
}
