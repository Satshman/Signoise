using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformEffectGA : GameAction
{
    public Effect Effect { get;  set; }
    public List<CombatanView> Targets { get; set; }
    public PerformEffectGA(Effect effect, List<CombatanView> targets)
    {
        Effect = effect;
        Targets = targets == null ? null:new(targets);
    }
}
