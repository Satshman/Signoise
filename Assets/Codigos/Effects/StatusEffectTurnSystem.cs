using UnityEngine;

public class StatusEffectTurnSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<EnemyTurnGA>(OnEnemyTurn, ReactionTiming.PRE);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(OnEnemyTurn, ReactionTiming.PRE);
    }

    private void OnEnemyTurn(EnemyTurnGA action)
    {
        foreach (var enemy in EnemySystem.Instance.Enemies)
        {
            enemy.TriggerTurnStartEffects();
        }

        HeroeSystem.Instance.HeroView.TriggerTurnStartEffects();
    }
}