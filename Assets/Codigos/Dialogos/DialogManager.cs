using UnityEngine;
using System;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    private Action onDialogoTerminado;

    private void Awake()
    {
        Instance = this;
    }

    public void IniciarDialogo(string id, Action callback = null)
    {
        onDialogoTerminado = callback;

        Debug.Log("Mostrando diálogo: " + id);

        // Aquí muestras UI (canvas, texto, etc)
        // Cuando termine el diálogo:
        Invoke(nameof(FinalizarDialogo), 2f); // ejemplo
    }

    void FinalizarDialogo()
    {
        onDialogoTerminado?.Invoke();
    }
}