using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") && GameManager.Instance != null)
        {
            GameManager.Instance.contadorColisiones++;
            Debug.Log("Contador de colisiones: " + GameManager.Instance.contadorColisiones);

            // Cambia de escena según el valor del contador
            switch (GameManager.Instance.contadorColisiones)
            {
                case 1:
                    SceneManager.LoadScene(1);
                    break;
                case 2:
                    SceneManager.LoadScene(0);
                    break;
                case 3:
                    SceneManager.LoadScene(4);
                    break;
                case 4:
                    SceneManager.LoadScene(2);
                    break;
                case 5:
                    SceneManager.LoadScene(5);
                    break;
                case 6:
                    SceneManager.LoadScene(2);
                    break;
                case 7:
                    SceneManager.LoadScene(6);
                    break;
                case 8:
                    SceneManager.LoadScene(7);
                    break;
            }
        }
    }
}
