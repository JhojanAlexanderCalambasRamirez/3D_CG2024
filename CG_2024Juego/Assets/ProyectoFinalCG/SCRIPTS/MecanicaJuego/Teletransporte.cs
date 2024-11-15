using UnityEngine;

public class Teletransporte : MonoBehaviour
{
    public GameObject destino; // Objeto de destino al que se teletransportará el jugador
    public string tagJugador = "Player1"; // Etiqueta del jugador
    private bool puedeTeletransportarse = true; // Controla el teletransporte inmediato

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona es el jugador
        if (other.CompareTag(tagJugador) && puedeTeletransportarse)
        {
            // Teletransporta al jugador a la posición del destino
            if (destino != null)
            {
                other.transform.position = destino.transform.position;

                // Desactiva temporalmente la capacidad de teletransportarse para evitar bucles
                destino.GetComponent<Teletransporte>().puedeTeletransportarse = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reactiva la capacidad de teletransportarse cuando el jugador sale del trigger
        if (other.CompareTag(tagJugador))
        {
            puedeTeletransportarse = true;
        }
    }
}
