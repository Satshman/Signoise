using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    public void Continuar()
    {
        PopupManager.Instance.Close("MenuOpciones");
    }

    public void AbrirOpciones()
    {
        PopupManager.Instance.Show("CartasOpciones");
        PopupManager.Instance.Close("MenuOpciones");
    }

}