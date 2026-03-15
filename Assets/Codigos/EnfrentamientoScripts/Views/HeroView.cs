using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroView : CombatanView
{
    public void Setup(HeroData heroData, int currentHP)
    {
        //SetupBase(heroData.Health, heroData.Image); real
        SetupBase(currentHP, heroData.Image);
    }

    //Animacion

}
