using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageGA : GameAction
{
    public int Amount { get; set; }
    public List<CombatanView> Targets { get; set; }

    public DealDamageGA(int amount,List<CombatanView> targets)
    {
        Amount = amount;
        Targets =new (targets);
    }
}
