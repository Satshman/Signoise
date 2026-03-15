using System.Collections.Generic;
using UnityEngine;

public class TargetHighlightSystem : MonoBehaviour
{
    public static TargetHighlightSystem Instance;

    private List<CombatanView> currentTargets = new();

    private void Awake()
    {
        Instance = this;
    }

    public void HighlightTargets(TargetMode targetMode)
    {
        if (targetMode == null)
        {
            Debug.Log("TargetMode es null");
            return;
        }

        List<CombatanView> targets = targetMode.GetTargets();

        if (targets == null)
        {
            Debug.Log("GetTargets devolvió null");
            return;
        }

        Debug.Log("Targets encontrados: " + targets.Count);

        foreach (var enemy in targets)
        {
            if (enemy != null)
            {
                enemy.SetHighlight(Color.red);
                currentTargets.Add(enemy);
            }
        }
    }

    public void ClearTargets()
    {
        foreach (var enemy in currentTargets)
        {
            if (enemy != null)
                enemy.ClearHighlight();
        }

        currentTargets.Clear();
    }
}