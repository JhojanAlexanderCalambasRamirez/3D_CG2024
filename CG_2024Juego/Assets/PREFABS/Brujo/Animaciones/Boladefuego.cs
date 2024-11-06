using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boladefuego : MonoBehaviour
{

    public float tiempoDeVida = 7f;  // Tiempo en segundos antes de que la bola de fuego se destruya

    private void Start()
    {
        // Asegúrate de que solo la bola de fuego se destruya después del tiempo de vida
        Debug.Log("Bola de fuego creada: " + gameObject.name);
        Destroy(gameObject, tiempoDeVida);  // Se destruye solo esta bola
    }

    private void OnDestroy()
    {
        Debug.Log("Bola de fuego destruida: " + gameObject.name);
    }
}
