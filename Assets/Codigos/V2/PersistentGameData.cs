using UnityEngine;
using System.Collections.Generic;

public class PersistentGameData : MonoBehaviour
{
    public static PersistentGameData Instance;

    [Header("Hero Stats")]
    public int heroCurrentHP;
    public int heroMaxHP;

    [Header("Pasillo Data")]
    public Vector3 lastPlayerPosition;
    public string currentEnemyID;
    public HashSet<string> defeatedEnemies = new HashSet<string>();

    [Header("Cinemáticas / Objetos")]
    public HashSet<string> cinematicsDone = new HashSet<string>();

    [Header("Inventario")]
    public HashSet<string> items = new HashSet<string>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(string itemID)
    {
        if (!items.Contains(itemID))
        {
            items.Add(itemID);
            Debug.Log("Item obtenido: " + itemID);
        }
    }

    public bool HasItem(string itemID)
    {
        return items.Contains(itemID);
    }
}
