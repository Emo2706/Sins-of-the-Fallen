using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI
{
    Player _player;
    Image _hpBar;
    float _fillAmountImage;

    public Player_UI(Image hpBar , Player player)
    {
        _hpBar = hpBar;
        _player = player;
    }

    public void Update()
    {
        _fillAmountImage = _player.life / 50f;
        _hpBar.fillAmount = _fillAmountImage;
        
    }

    
}
