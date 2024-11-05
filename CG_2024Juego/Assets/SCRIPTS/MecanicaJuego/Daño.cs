using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daño : MonoBehaviour
{
    public float cantidadDaño = 2f;  // Cantidad de daño que hará al jugador

    // Este método se activa cuando otro objeto con trigger entra en el collider
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que colisiona tiene el tag "Player"
        if (other.CompareTag("Player1"))
        {
            // Obtiene el componente Vida del jugador
            Vida vidaJugador = other.GetComponent<Vida>();

            // Si el jugador tiene el componente Vida, aplica el daño
            if (vidaJugador != null)
            {
                vidaJugador.RecibirDaño(cantidadDaño);
            }
            else
            {
                Debug.LogWarning("El jugador no tiene un componente de Vida.");
            }
        }
    }
}
