
using UnityEngine;

/**
 * Clase que activa el arma de un personaje al entrar en contacto con el jugador.
 * Verifica si el jugador tiene el componente `CogerArmas` para registrar el arma en el inventario.
 * @author Alexander Calambas - 2190555
//Juan Manuel Santos - 2215928
//Juan David Rios - 2225674
 * @date 11 noviembre 2024
 * @version 1.0
 */

public class ActivarArmaPersonaje : MonoBehaviour
{
  /** Referencia al componente CogerArmas del jugador, que gestiona el inventario de armas. */
    public CogerArmas cogerArmas;

   /** Número del arma en el array para identificarla. */
    public int numeroArma;

    /**
    * Método Start que se ejecuta al inicio. Busca al objeto con el tag "Player1" y obtiene el componente CogerArmas.
    */
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player1");
        if (player != null)
        {
            cogerArmas = player.GetComponent<CogerArmas>();
        }
        else
        {
            Debug.LogError("No se encontró un objeto con el tag 'Player1' en la escena.");
        }
    }

    /**
     * Método que se llama cuando otro collider entra en contacto con el objeto que contiene este script.
     * Verifica si el collider pertenece al jugador y, si es así, marca el arma como recogida y la desactiva en la escena.
     * @param other Collider del objeto con el que colisiona.
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") && cogerArmas != null)
        {
            cogerArmas.RecogerArma(numeroArma);  // Marca el arma como recogida en el inventario
            gameObject.SetActive(false); // Solo desactiva el arma en la escena
        }
    }
}
