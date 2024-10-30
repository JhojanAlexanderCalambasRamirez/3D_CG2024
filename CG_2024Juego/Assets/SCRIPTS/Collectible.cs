//Alexander Calambas - 2190555
//Juan Manuel Santos - 2215928
//Juan David Rios - 2225674

using UnityEngine;

public class Collectible : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        // Intentar obtener el GameManager automáticamente
        gameManager = GameManager.instance;

        if (gameManager == null)
        {
            Debug.LogError("GameManager no está presente en la escena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                gameManager.AddPoints(10);  // Agregar puntos al jugador
                gameManager.AddItem(1);     // Agregar ítems al jugador
                Destroy(gameObject);        // Destruir el objeto recolectable
            }
            else
            {
                Debug.LogError("GameManager no está asignado o encontrado.");
            }
        }
    }
}
