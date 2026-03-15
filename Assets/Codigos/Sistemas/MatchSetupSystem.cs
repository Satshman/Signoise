using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    [SerializeField] private HeroData heroData;
    [SerializeField] private List<EnemyData> enemyDatas;

    private void Start()
    {
        // --- 1) Resetear totalmente ActionSystem ---
        ActionSystem.ResetSubscriptions();

        // --- 2) Forzar reactivación del CardSystem para que vuelva a registrar performers ---
        CardSystem.Instance.enabled = false;
        CardSystem.Instance.enabled = true;

        // --- 3) Resetear cartas y estado interno ---
        CardSystem.Instance.ResetSystem();

        // --- 4) Configurar vida persistente ---
        int baseHP = heroData.Health;

        if (PersistentGameData.Instance.heroMaxHP <= 0)
        {
            PersistentGameData.Instance.heroMaxHP = baseHP;
            PersistentGameData.Instance.heroCurrentHP = baseHP;
        }

        // Heroe con vida persistente
        HeroeSystem.Instance.Setup(heroData, PersistentGameData.Instance.heroCurrentHP);

        // --- 5) Setup enemigos ---
        EnemySystem.Instance.Setup(enemyDatas);

        // --- 6) Setup del deck ---
        CardSystem.Instance.Setup(heroData.Deck);

        // --- 7) Robar 6 cartas iniciales ---
        ActionSystem.Instance.Perform(new DrawCardsGa(6));
    }
}
