using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CombatanView : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // NUEVO
    [SerializeField] private SpriteRenderer highlightRenderer;

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }

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

    public void Damage(int damageAmount)
    {
        CurrentHealth -= damageAmount;

        if (CurrentHealth < 0)
            CurrentHealth = 0;

        transform.DOShakePosition(0.2f, 0.5f);
        UpdateHealthText();
    }

    // HIGHLIGHT SYSTEM

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

    // Animación muerte
    public IEnumerator PlayDeathAnimation()
    {
        transform.DOShakePosition(0.2f, 0.5f);
        yield return new WaitForSeconds(0.3f);

        if (spriteRenderer != null)
            spriteRenderer.DOFade(0, 0.4f);

        yield return new WaitForSeconds(0.4f);
    }
}