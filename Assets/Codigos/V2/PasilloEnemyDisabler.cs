using UnityEngine;

public class PasilloEnemyDisabler : MonoBehaviour
{
    [SerializeField] private string enemyID;

    private void Start()
    {
        if (PersistentGameData.Instance.defeatedEnemies.Contains(enemyID))
        {
            gameObject.SetActive(false);
        }
    }
}
