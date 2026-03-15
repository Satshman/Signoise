using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroeSystem : Singleton<HeroeSystem>
{
    [field:SerializeField] public HeroView HeroView {  get; private set; }
    public void Setup(HeroData heroData, int currentHP)
    {
        HeroView.Setup(heroData, currentHP);
    }
}
