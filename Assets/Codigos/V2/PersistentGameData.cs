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
}
