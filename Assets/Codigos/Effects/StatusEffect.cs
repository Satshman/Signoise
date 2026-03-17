public abstract class StatusEffect
{
    public int Duration { get; protected set; }

    public StatusEffect(int duration)
    {
        Duration = duration;
    }

    public virtual void OnApply(CombatanView target) { }

    public virtual void OnTurnStart(CombatanView target) { }

    public virtual void OnTurnEnd(CombatanView target) { }

    protected void ReduceDuration()
    {
        Duration--;
    }

    public bool IsFinished()
    {
        return Duration <= 0;
    }
}