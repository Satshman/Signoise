using UnityEngine;

public class ClosePopupWithEscape : MonoBehaviour
{
    public string popupID;

    void Update()
    {
        if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            PopupManager.Instance.Close(popupID);
            PopupManager.Instance.Show("MenuOpciones");
        }
    }
}