using UnityEngine;
using UnityEngine.SceneManagement;

public enum TipoInteraccion
{
    NPC,
    Enemigo,
    Objeto
}

public class Interaccion : MonoBehaviour
{
    private bool jugadorCerca = false;
    private bool didDialogueStart = false;

    [Header("Tipo de interacción")]
    [SerializeField] private TipoInteraccion tipo;

    [Header("Dialogo")]
    [SerializeField] private LineaDialogo[] dialogueLines;

    [Header("Retratos (se envían al UI central)")]
    [SerializeField] private Sprite spriteIzquierda;
    [SerializeField] private Sprite spriteDerecha;

    [Header("UI Indicador")]
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private string uniqueID;

    [Header("Combate (si es enemigo)")]
    [SerializeField] private string enemyID;
    [SerializeField] private string combateScene = "Enfrentamiento";

    [Header("Recompensas")]
    [SerializeField] private string itemID; 

    private void Start()
    {
        if (tipo == TipoInteraccion.Objeto)
        {
            if (PersistentGameData.Instance.cinematicsDone.Contains(uniqueID))
            {
                Destroy(gameObject);
                return;
            }
        }

        if (dialogueMark != null)
            dialogueMark.SetActive(false);
    }

    private void Update()
    {

        if (jugadorCerca && !didDialogueStart &&
            (tipo == TipoInteraccion.Enemigo || tipo == TipoInteraccion.Objeto))
        {
            StartDialogue();
        }

        if (jugadorCerca && tipo == TipoInteraccion.NPC && Input.GetKeyDown(KeyCode.E))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;

        if (dialogueMark != null)
            dialogueMark.SetActive(false);


        DialogoUI.Instance.IniciarDialogo(
            dialogueLines,
            spriteIzquierda,
            spriteDerecha,
            OnDialogoTerminado
        );
    }

    private void OnDialogoTerminado()
    {
        didDialogueStart = false;

        if (!string.IsNullOrEmpty(itemID) &&
        !PersistentGameData.Instance.HasItem(itemID))
        {
            PersistentGameData.Instance.AddItem(itemID);
        }

        switch (tipo)
        {
            case TipoInteraccion.NPC:
                break;

            case TipoInteraccion.Enemigo:
                CombateChange();
                break;

            case TipoInteraccion.Objeto:

                if (!string.IsNullOrEmpty(uniqueID))
                {
                    PersistentGameData.Instance.cinematicsDone.Add(uniqueID);
                }

                Destroy(gameObject);
                break;
        }
    }

    private void CombateChange()
    {
        // Guardar posición del jugador
        PersistentGameData.Instance.lastPlayerPosition =
            GameObject.FindWithTag("Player").transform.position;

        // Guardar enemigo
        PersistentGameData.Instance.currentEnemyID = enemyID;

        // Cambiar escena
        SceneManager.LoadScene(combateScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;

            if (dialogueMark != null && tipo == TipoInteraccion.NPC)
                dialogueMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;

            if (dialogueMark != null)
                dialogueMark.SetActive(false);
        }
    }
}