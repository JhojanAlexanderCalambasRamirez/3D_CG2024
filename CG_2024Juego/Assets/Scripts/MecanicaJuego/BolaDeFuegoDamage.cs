using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BolaDeFuegoDamage : MonoBehaviour
{
    public float daño = 10.0f; // Ajusta el valor del daño según lo que prefieras

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            // Busca el componente de vida en el jugador y aplica daño
            Vida vidaJugador = collision.gameObject.GetComponent<Vida>();
            if (vidaJugador != null)
            {
                vidaJugador.RecibirDaño(daño, true); // Indica que es ataque del jefe
            }

            // Destruir la bola de fuego tras el impacto
            Destroy(gameObject);
        }
    }
}

