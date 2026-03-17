using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CombatanView : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private SpriteRenderer highlightRenderer;

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

    // NUEVO
    public int CurrentShield { get; private set; }

    private List<StatusEffect> statusEffects = new();

    protected void SetupBase(int health, Sprite image)
    {
        MaxHealth = CurrentHealth = health;
        spriteRenderer.sprite = image;

        UpdateHealthText();
        ClearHighlight();
    }

    private void UpdateHealthText()
    {
        healthText.text = "HP:" + CurrentHealth;
    }

    // =========================
    // DAMAGE
    // =========================

    public void Damage(int damageAmount)
    {
        int remainingDamage = damageAmount;

        if (CurrentShield > 0)
        {
            int shieldDamage = Mathf.Min(CurrentShield, remainingDamage);
            CurrentShield -= shieldDamage;
            remainingDamage -= shieldDamage;
        }

        if (remainingDamage > 0)
        {
            CurrentHealth -= remainingDamage;
        }

        if (CurrentHealth < 0)
            CurrentHealth = 0;

        transform.DOShakePosition(0.2f, 0.5f);

        UpdateHealthText();
    }

    // =========================
    // SHIELD
    // =========================

    public void GainShield(int amount)
    {
        CurrentShield += amount;
    }

    // =========================
    // STATUS EFFECTS
    // =========================

    public void AddStatusEffect(StatusEffect effect)
    {
        statusEffects.Add(effect);
        effect.OnApply(this);
    }

    public void TriggerTurnStartEffects()
    {
        foreach (var effect in statusEffects)
        {
            effect.OnTurnStart(this);
        }

        statusEffects.RemoveAll(e => e.IsFinished());
    }

    public void TriggerTurnEndEffects()
    {
        foreach (var effect in statusEffects)
        {
            effect.OnTurnEnd(this);
        }

        statusEffects.RemoveAll(e => e.IsFinished());
    }

    // =========================
    // HIGHLIGHT SYSTEM
    // =========================

    public void SetHighlight(Color color)
    {
        if (spriteRenderer == null) return;

        spriteRenderer.color = color;
    }

    public void ClearHighlight()
    {
        if (spriteRenderer == null) return;

        spriteRenderer.color = Color.white;
    }

    // =========================
    // ANIMACION MUERTE
    // =========================

    public IEnumerator PlayDeathAnimation()
    {
        transform.DOShakePosition(0.2f, 0.5f);

        yield return new WaitForSeconds(0.3f);

        if (spriteRenderer != null)
            spriteRenderer.DOFade(0, 0.4f);

        yield return new WaitForSeconds(0.4f);
    }
}