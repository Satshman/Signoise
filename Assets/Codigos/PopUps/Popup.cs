using UnityEngine;

public class Popup : MonoBehaviour
{
    public string popupID;

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}