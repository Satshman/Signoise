using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum TipoInteraccion
{
    NPC,
    Enemigo,
    Objeto
}
public class Interaccion : MonoBehaviour
{
    private bool jugadorCerca = false;

    [Header("UI")]
    [SerializeField] private GameObject dialogueMark;   // icono encima (E)
    [SerializeField] private GameObject dialoguePanel;  // panel del diálogo
    [SerializeField] private TMP_Text dialogueText;


    [Header("Dialogo")]
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    private bool didDialogueStart;
    private int lineIndex;
    private float typing = 0.05f;
    private bool isTyping;

    [Header("Combate (opcional)")]
    [SerializeField] private TipoInteraccion tipo;
    [SerializeField] private string enemyID;
    [SerializeField] private string combateScene = "Enfrentamiento";

    private void Start()
    {
        dialoguePanel.SetActive(false);
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
            else if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
                isTyping = false;
            }
            else
            {
                NextDialogueLine();
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false); // ocultar icono mientras habla
        lineIndex = 0;
        GameManager.Instance.puedeMoverse = false;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;

        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        didDialogueStart = false;

        dialoguePanel.SetActive(false);
        dialogueMark.SetActive(true);

        GameManager.Instance.puedeMoverse = true;

        switch (tipo)
        {
            case TipoInteraccion.NPC:
                break;

            case TipoInteraccion.Enemigo:
                CombateChange();
                break;

            case TipoInteraccion.Objeto:
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

    private IEnumerator ShowLine()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typing);
        }

        isTyping = false;

   
        if (tipo == TipoInteraccion.Enemigo || tipo == TipoInteraccion.Objeto)
        {
            yield return new WaitForSeconds(1f); // tiempo de lectura
            NextDialogueLine();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
            dialogueMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
            dialogueMark.SetActive(false);
        }
    }
}