//Alexander Calambas - 2190555
//Juan Manuel Santos - 2215928
//Juan David Rios - 2225674


using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona tiene el tag "Player1"
        if (other.CompareTag("Player1"))
        {
            // Cambia a la escena con índice 0
            SceneManager.LoadScene(0);
        }
        else if (other.CompareTag("Player2"))
        {
            // Cambia a la escena con índice 1
            SceneManager.LoadScene(1);
        }
    }
}
