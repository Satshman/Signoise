using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemiesTM : TargetMode
{
    public override List<CombatanView> GetTargets() 
    {
        return new(EnemySystem.Instance.Enemies);
    }
}
