

using UnityEngine;
using UnityEngine.SceneManagement;

/**
* Clase que maneja el cambio de escena al finalizar el juego.
* Detecta cuando el jugador llega a un área de cambio de escena y guarda el progreso antes de cargar la escena final.
* @author Alexander Calambas - 2190555
* @author Juan Manuel Santos - 2215928
* @author Juan David Rios - 2225674
* @date 11 noviembre 2024
* @version 1.0
*/

public class cambioFinal : MonoBehaviour
{
    private Vector2 puntoInicial;  // Para recordar la posición inicial del objeto

    /**
  * Método de inicialización que guarda la posición inicial del objeto al comenzar.
  * @param Ninguno
  * @return Ninguno
  */
    void Start()
    {
        puntoInicial = transform.position;  // Guardar la posición inicial
    }

    /**
    * Método que se ejecuta cuando un objeto entra en el área de colisión del objeto que tiene este script.
    * Si el objeto es el jugador, guarda el progreso y cambia a la escena final.
    * @param other El objeto que colisiona con el área de cambio (debe ser el jugador).
    * @return Ninguno
    */

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Guardar progreso antes de cambiar a la escena final
            GameManager.instance.SaveProgress();
            SceneManager.LoadScene("EscenaFinal");  // Cambiar a la escena final
        }
    }
}
