using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float daño = 20f; // Daño que se le hará al enemigo
    public float tiempoEntreAtaques = 1.0f; // Tiempo de espera entre ataques en segundos
    public string tagEnemigo = "Enemigo"; // Tag que identifica a los enemigos

    private VidaEnemigo enemigoActual; // Referencia al enemigo actual que se está atacando
    private float tiempoSiguienteAtaque = 0f; // Tiempo en el que se puede atacar de nuevo

    void Update()
    {
        // Verifica si el jugador presiona la tecla "E" y si ya pasó el tiempo de espera
        if (Input.GetKeyDown(KeyCode.E) && enemigoActual != null && Time.time >= tiempoSiguienteAtaque)
        {
            // Realiza el ataque y actualiza el tiempo para el próximo ataque
            enemigoActual.RecibirDaño(daño);
            tiempoSiguienteAtaque = Time.time + tiempoEntreAtaques;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si el objeto con el que colisionamos tiene el tag de enemigo, lo guardamos
        if (other.CompareTag(tagEnemigo))
        {
            enemigoActual = other.GetComponent<VidaEnemigo>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si el jugador sale del área de colisión con el enemigo, reseteamos la referencia
        if (other.CompareTag(tagEnemigo))
        {
            enemigoActual = null;
        }
    }
}
