using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem : Singleton<ManaSystem>
{
    [SerializeField] private ManaUi manaUi;
    private const int MAX_MANA = 3;
    private int currentMana = MAX_MANA;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<SpendManaGA>(SpendManaPerformer);
        ActionSystem.AttachPerformer<RefillManaGA>(RefillManaPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);

    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<SpendManaGA>();
        ActionSystem.DetachPerformer<RefillManaGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    public bool HasEnoughMana(int mana)
    {
        return  currentMana >=mana;
    }

    private IEnumerator SpendManaPerformer(SpendManaGA spendManaGa)
    {
        currentMana -= spendManaGa.Amount;
        manaUi.UpdateManaText(currentMana);
        yield return null;
    }

    private IEnumerator RefillManaPerformer(RefillManaGA refillManaGA)
    {
        currentMana=MAX_MANA;
        manaUi.UpdateManaText(currentMana);
        yield return null;
    }

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        RefillManaGA refillmanaGA = new();
        ActionSystem.Instance.AddReaction(refillmanaGA);
    }
}
