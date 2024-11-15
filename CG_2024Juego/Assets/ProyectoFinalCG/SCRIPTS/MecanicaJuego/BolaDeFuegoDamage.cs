using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BolaDeFuegoDamage : MonoBehaviour
{
    public float daño = 10.0f; // Ajusta el valor del daño según lo que prefieras

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            Vida vidaJugador = other.GetComponent<Vida>();
            if (vidaJugador != null)
            {
                vidaJugador.RecibirDaño(daño, true); // Ajusta el daño
            }

            Destroy(gameObject);
        }
    }

}

