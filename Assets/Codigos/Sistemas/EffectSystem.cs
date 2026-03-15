using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<PerformEffectGA>(PerformEffectPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<PerformEffectGA>();
    }

    private IEnumerator PerformEffectPerformer(PerformEffectGA performEffectGA)
    {
        GameAction effecAction = performEffectGA.Effect.GetGameAction(performEffectGA.Targets);
        ActionSystem.Instance.AddReaction(effecAction);
        yield return null;
    }
}
