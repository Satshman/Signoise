using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageCardEffect : Effect
{
    [SerializeField] private int damageAmount;

    public override GameAction GetGameAction(List<CombatanView> targets)
    {
        DealDamageGA dealDamageGA = new(damageAmount,targets);
        return dealDamageGA;
    }
}
