using System.Collections;
using UnityEngine;

public class ApplyStatusEffectSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<ApplyStatusEffectGA>(ApplyEffect);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<ApplyStatusEffectGA>();
    }

    private IEnumerator ApplyEffect(ApplyStatusEffectGA action)
    {
        foreach (var target in action.Targets)
        {
            target.AddStatusEffect(action.Effect);
        }

        yield return null;
    }
}