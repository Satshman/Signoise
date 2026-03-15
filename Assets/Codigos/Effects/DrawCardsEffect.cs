using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardsEffect : Effect
{
    [SerializeField] private int drawAmount;
    public override GameAction GetGameAction(List<CombatanView> targets)
    {
        DrawCardsGa drawCardsGa = new(drawAmount);
        return drawCardsGa;
    }
}
