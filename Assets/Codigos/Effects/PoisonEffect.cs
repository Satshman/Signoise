using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : Effect
{
    [SerializeField] private int damage;
    [SerializeField] private int turns;

    public override GameAction GetGameAction(List<CombatanView> targets)
    {
        PoisonStatusEffect poison = new(damage, turns);

        return new ApplyStatusEffectGA(poison, targets);
    }
}