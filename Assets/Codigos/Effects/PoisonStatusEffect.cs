using UnityEngine;

public class PoisonStatusEffect : StatusEffect
{
    int damage;

    public PoisonStatusEffect(int damage, int duration) : base(duration)
    {
        this.damage = damage;
    }

    public override void OnTurnStart(CombatanView target)
    {
        target.Damage(damage);

        Debug.Log("Poison damage: " + damage);

        ReduceDuration();
    }
}