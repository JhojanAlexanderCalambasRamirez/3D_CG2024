using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daño : MonoBehaviour
{
    public float cantidadDaño = 2f;  // Cantidad de daño que hará al jugador
    public float intervaloDaño = 1f; // Intervalo de tiempo entre cada aplicación de daño

    private bool enContactoConJugador = false;
    private Vida vidaJugador;
    private float tiempoSiguienteDaño = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            // Obtiene el componente Vida del jugador
            vidaJugador = other.GetComponent<Vida>();

            if (vidaJugador != null)
            {
                enContactoConJugador = true;
            }
            else
            {
                Debug.LogWarning("El jugador no tiene un componente de Vida.");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (enContactoConJugador && Time.time >= tiempoSiguienteDaño)
        {
            vidaJugador.RecibirDaño(cantidadDaño);
            tiempoSiguienteDaño = Time.time + intervaloDaño; // Define el próximo momento para aplicar daño
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            enContactoConJugador = false; // Detiene el daño cuando el jugador sale del trigger
        }
    }
}
