using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightAndMiddleEnemyTM : TargetMode
{
    public override List<CombatanView> GetTargets()
    {
        var enemies = EnemySystem.Instance.Enemies;

        if (enemies == null || enemies.Count == 0)
            return null;

        List<CombatanView> targets = new();

        int middleIndex = enemies.Count / 2;

        targets.Add(enemies[enemies.Count - 1]); // derecha

        if (middleIndex != enemies.Count - 1)
            targets.Add(enemies[middleIndex]); // centro

        return targets;
    }
}