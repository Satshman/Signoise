using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public enum Hablante
{
    Personaje1,
    Personaje2
}

[System.Serializable]
public class LineaDialogo
{
    public string texto;
    public Hablante hablante;
}
public class DialogoUI : MonoBehaviour
{
    public static DialogoUI Instance;

    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text texto;
    [SerializeField] private Image izquierda;
    [SerializeField] private Image derecha;

    private float typing = 0.05f;
    private bool isTyping;
    private int lineIndex;
    private LineaDialogo[] lineasActuales;

    private Action onFinish;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void IniciarDialogo(LineaDialogo[] lineas, Sprite spriteIzq, Sprite spriteDer, Action callback = null)
    {
        lineasActuales = lineas;
        onFinish = callback;

        // Asignar sprites
        izquierda.sprite = spriteIzq;
        derecha.sprite = spriteDer;

        if (izquierda != null)
            izquierda.gameObject.SetActive(spriteIzq != null);

        if (derecha != null)
            derecha.gameObject.SetActive(spriteDer != null);

        lineIndex = 0;
        panel.SetActive(true);

        GameManager.Instance.puedeMoverse = false;

        StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine()
    {
        isTyping = true;
        texto.text = "";

        if (lineasActuales[lineIndex].hablante == Hablante.Personaje1)
        {
            izquierda.color = Color.white;
            derecha.color = new Color(0.3f, 0.3f, 0.3f);
        }
        else
        {
            izquierda.color = new Color(0.3f, 0.3f, 0.3f);
            derecha.color = Color.white;
        }

        foreach (char c in lineasActuales[lineIndex].texto)
        {
            texto.text += c;
            yield return new WaitForSeconds(typing);
        }

        isTyping = false;

        yield return new WaitForSeconds(1f);
        NextLine();
    }

    private void NextLine()
    {
        lineIndex++;

        if (lineIndex < lineasActuales.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            EndDialogo();
        }
    }

    private void EndDialogo()
    {
        panel.SetActive(false);

        izquierda.gameObject.SetActive(false);
        derecha.gameObject.SetActive(false);

        GameManager.Instance.puedeMoverse = true;

        onFinish?.Invoke();
    }

    //Nuevo
    public void MostrarMensaje(string mensaje, float duracion = 2f)
    {
        StopAllCoroutines();
        StartCoroutine(MostrarMensajeCoroutine(mensaje, duracion));
    }

    private IEnumerator MostrarMensajeCoroutine(string mensaje, float duracion)
    {
        panel.SetActive(true);
        texto.text = mensaje;

        izquierda.gameObject.SetActive(false);
        derecha.gameObject.SetActive(false);

        GameManager.Instance.puedeMoverse = false;

        yield return new WaitForSeconds(duracion);

        panel.SetActive(false);
        GameManager.Instance.puedeMoverse = true;
    }
}