using System.Collections.Generic;

public class ApplyStatusEffectGA : GameAction
{
    public StatusEffect Effect { get; private set; }
    public List<CombatanView> Targets { get; private set; }

    public ApplyStatusEffectGA(StatusEffect effect, List<CombatanView> targets)
    {
        Effect = effect;
        Targets = targets;
    }
}