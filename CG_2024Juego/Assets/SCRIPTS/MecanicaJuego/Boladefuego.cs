using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Clase que controla el comportamiento de una bola de fuego en el juego.
 * Detecta colisiones y destruye la bola de fuego al impactar con el jugador.
 * @author  @author Alexander Calambas - 2190555
//Juan Manuel Santos - 2215928
//Juan David Rios - 2225674
 * @date 11 novirmbre  2024
 * @version 1.0
 */

public class Boladefuego : MonoBehaviour

{

   /**
     * Método para detectar la colisión física de la bola de fuego con otros objetos.
     * Si la bola de fuego impacta al jugador, se destruye a sí misma.
     * @param collision Información sobre el objeto con el que colisiona.
     */
    void OnCollisionEnter(Collision collision)
    {
        // Verifica si la bola de fuego ha chocado con el jugador
        if (collision.gameObject.CompareTag("Player1"))
        {
            // Destruye la bola de fuego al impactar con el jugador
            Destroy(gameObject);
        }
    }
}
