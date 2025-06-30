using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerView
{
    Animator _anim;
    float _smoothValue;
   
    public PlayerView(Player player)
    {
        _anim = player.armAnim;
        _smoothValue = player.smoothValue;
    }

    public void BlendAnimations(float blendValue)
    {
        DOTween.To(() => _anim.GetFloat("Charge"), x => _anim.SetFloat("Charge", x), blendValue,_smoothValue);
    }

    public void Shoot()
    {
        _anim.SetTrigger("Shoot");
    }
}
