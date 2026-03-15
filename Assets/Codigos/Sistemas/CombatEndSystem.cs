using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CombatEndSystem : Singleton<CombatEndSystem>
{
    [Header("Scenes to Load")]
    [SerializeField] private string winScene = "Pasillo";
    [SerializeField] private string loseScene = "Menu";

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<KillEnemyGA>(OnEnemyKilled, ReactionTiming.POST);
        ActionSystem.SubscribeReaction<DealDamageGA>(OnDamageDealt, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<KillEnemyGA>(OnEnemyKilled, ReactionTiming.POST);
        ActionSystem.UnsubscribeReaction<DealDamageGA>(OnDamageDealt, ReactionTiming.POST);
    }

    // --- CUANDO UN ENEMIGO MUERE ---
    private void OnEnemyKilled(KillEnemyGA ga)
    {
        // Protección anti MissingReferenceException
        if (this == null || !this.gameObject) return;

        StartCoroutine(CheckEnemiesAfterFrame());
    }

    private IEnumerator CheckEnemiesAfterFrame()
    {
        yield return null;

        // Segunda protección
        if (this == null || !this.gameObject) yield break;

        if (EnemySystem.Instance.Enemies.Count == 0)
        {
            Debug.Log("GANASTE — No quedan enemigos.");
            EndCombat(true);
        }
    }

    // --- CUANDO SE HACE DAÑO ---
    private void OnDamageDealt(DealDamageGA ga)
    {
        // Protección anti objeto destruido
        if (this == null || !this.gameObject) return;

        foreach (var target in ga.Targets)
        {
            if (target == HeroeSystem.Instance.HeroView)
            {
                if (HeroeSystem.Instance.HeroView.CurrentHealth <= 0)
                {
                    Debug.Log("PERDISTE — El héroe murió.");
                    StartCoroutine(HeroDeathSequence());
                }
            }
        }
    }

    private IEnumerator HeroDeathSequence()
    {
        yield return HeroeSystem.Instance.HeroView.PlayDeathAnimation();

        // Protege por si se destruye durante la animación
        if (this == null || !this.gameObject) yield break;

        EndCombat(false);
    }

    // --- FINALIZAR COMBATE ---
    private void EndCombat(bool win)
    {
        // Evitar cortar acciones en proceso
        if (ActionSystem.Instance.IsPerforming)
        {
            StartCoroutine(WaitAndEnd(win));
            return;
        }

        LoadScene(win);
    }

    private IEnumerator WaitAndEnd(bool win)
    {
        yield return new WaitUntil(() => !ActionSystem.Instance.IsPerforming);

        // Protección final
        if (this == null || !this.gameObject) yield break;

        LoadScene(win);
    }

    private void LoadScene(bool win)
    {
        // Guardar vida actual del héroe
        if (HeroeSystem.Instance != null && HeroeSystem.Instance.HeroView != null)
        {
            PersistentGameData.Instance.heroCurrentHP = 20;
        }

        // MARCAR ENEMIGO COMO DERROTADO
        if (win && !string.IsNullOrEmpty(PersistentGameData.Instance.currentEnemyID))
        {
            PersistentGameData.Instance.defeatedEnemies.Add(PersistentGameData.Instance.currentEnemyID);
            PersistentGameData.Instance.currentEnemyID = "";
        }

        SceneManager.LoadScene(win ? winScene : loseScene);
    }


    //Nuevo
    private void Awake()
    {
        ActionSystem.OnReset += ForceResubscribe;
    }

    private void OnDestroy()
    {
        ActionSystem.OnReset -= ForceResubscribe;
    }

    private void ForceResubscribe()
    {
        OnDisable(); // desuscribirse
        OnEnable();  // suscribirse de nuevo
    }
}
