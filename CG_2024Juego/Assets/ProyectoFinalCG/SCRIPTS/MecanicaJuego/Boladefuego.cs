using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boladefuego : MonoBehaviour
{
    // Método para detectar la colisión física
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
