using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    private Dictionary<string, Popup> popups = new Dictionary<string, Popup>();

    public string escapePopupID = "MenuOpciones"; 

    private bool isOpen = false;

    private void Awake()
    {
        Instance = this;

        Popup[] popupArray = GetComponentsInChildren<Popup>(true);

        foreach (Popup popup in popupArray)
        {
            popups.Add(popup.popupID, popup);
            popup.Close();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOpen)
            {
                Show(escapePopupID);
                isOpen = true;
            }
            else
            {
                Close(escapePopupID);
                isOpen = false;
            }
        }
    }

    public void Show(string id)
    {
        if (popups.ContainsKey(id))
        {
            popups[id].Open();
        }
    }

    public void Close(string id)
    {
        if (popups.ContainsKey(id))
        {
            popups[id].Close();
        }
    }
}