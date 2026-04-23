using UnityEngine;

public class Puerta : MonoBehaviour
{
    private bool jugadorCerca = false;

    [Header("Configuración")]
    [SerializeField] private string itemNecesario = "key_roja";

    [Header("Opcional")]
    [SerializeField] private GameObject puertaVisual;
    [SerializeField] private bool destruirAlAbrir = true;

    private void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            IntentarAbrir();
        }
    }

    private void IntentarAbrir()
    {
        if (PersistentGameData.Instance.HasItem(itemNecesario))
        {
            if (destruirAlAbrir)
            {
                Destroy(gameObject);
            }
            else if (puertaVisual != null)
            {
                puertaVisual.SetActive(false);
            }
        }
        else
        {
            DialogoUI.Instance.MostrarMensaje("Necesitas la llave roja");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
        }
    }
}