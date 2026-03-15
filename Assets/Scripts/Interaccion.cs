using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaccion : MonoBehaviour
{
    private bool jugadorCerca = false;

    [SerializeField] private string enemyID;
    [SerializeField] private string combateScene = "Enfrentamiento";

    private void Update()
    {
        if (jugadorCerca)
        {
            // Guardar posición actual del jugador
            PersistentGameData.Instance.lastPlayerPosition = GameObject.FindWithTag("Player").transform.position;

            // Guardar el ID del enemigo
            PersistentGameData.Instance.currentEnemyID = enemyID;

            // Cargar escena de combate
            SceneManager.LoadScene(combateScene);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            jugadorCerca = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            jugadorCerca = false;
    }
}
