using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarArmaPersonaje : MonoBehaviour
{
    public CogerArmas cogerArmas;
    public int numeroArma;  // Número del arma en el array para identificarla

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") && cogerArmas != null)
        {
            cogerArmas.RecogerArma(numeroArma);  // Marca el arma como recogida en el inventario
            Destroy(gameObject);  // Destruye el arma en el escenario después de recogerla
        }
    }
}
