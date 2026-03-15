using UnityEngine;

public class EnemyPasillo : MonoBehaviour
{
    [SerializeField] private string enemyID;

    private void Start()
    {
        // Si este enemigo ya fue derrotado, se desactiva
        if (PersistentGameData.Instance.defeatedEnemies.Contains(enemyID))
        {
            gameObject.SetActive(false);
        }
    }

    // Método público para marcar este enemigo como derrotado
    public void MarkAsDefeated()
    {
        if (!string.IsNullOrEmpty(enemyID))
        {
            PersistentGameData.Instance.defeatedEnemies.Add(enemyID);
        }
        gameObject.SetActive(false); // opcional, si quieres que desaparezca inmediatamente
    }
}
