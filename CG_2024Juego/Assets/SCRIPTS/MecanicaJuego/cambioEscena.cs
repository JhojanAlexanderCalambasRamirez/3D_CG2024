

using UnityEngine;
using UnityEngine.SceneManagement;
/**
* Clase que maneja el cambio de escenas cuando el jugador entra en una zona específica.
* Controla el cambio de escenas en función de la cantidad de colisiones que el jugador tiene con el área de activación.
* @author Alexander Calambas - 2190555
* @author Juan Manuel Santos - 2215928
* @author Juan David Rios - 2225674
* @date 11 noviembre 2024
* @version 1.0
*/

public class CambioEscena : MonoBehaviour
{
    private static int contadorColisiones = 0; // Variable estática para que el contador se mantenga entre escenas

    /**
   * Método que se ejecuta cuando un objeto entra en el área de colisión del objeto que tiene este script.
   * Si el objeto es el jugador, incrementa el contador de colisiones y carga una escena dependiendo del valor del contador.
   * @param other El objeto que colisiona con el área de activación (debe ser el jugador).
   * @return Ninguno
   */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            contadorColisiones++; // Incrementa el contador en cada colisión

            // Cambia de escena según el valor del contador
            switch (contadorColisiones)
            {
                case 1: // Primera colisión, carga la escena Mina
                    SceneManager.LoadScene(1);
                    break;
                case 2: // Segunda colisión, carga la escena MenuPrincipal
                    SceneManager.LoadScene(0);
                    break;
                case 3: // Tercera colisión, carga la escena DemoRed
                    SceneManager.LoadScene(4);
                    break;
                case 4: // Cuarta colisión, carga la escena Bosque
                    SceneManager.LoadScene(2);
                    break;
                case 5: // Quinta colisión, carga la escena DemoGreen
                    SceneManager.LoadScene(5);
                    break;
                case 6: // Sexta colisión, carga la escena Bosque
                    SceneManager.LoadScene(2);
                    break;
                case 7: // Séptima colisión, carga la escena DemoBlue
                    SceneManager.LoadScene(6);
                    break;
                case 8: // Octava colisión, carga la escena Creditos/Final
                    SceneManager.LoadScene(7);
                    break;
            }
        }
    }
}
