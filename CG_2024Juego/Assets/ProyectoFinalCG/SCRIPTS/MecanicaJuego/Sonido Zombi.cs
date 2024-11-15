using UnityEngine;

public class SonidoZombi : MonoBehaviour
{
    public AudioSource fuenteAudio;
    public Transform player;  // Asigna el Transform del jugador en el Inspector
    public float distanciaActivacion = 2f; 
    public float distanciaDesactivacion = 2f; 
    private bool sonidoActivo = false;

    void Start()
    {
        if (fuenteAudio == null)
        {
            fuenteAudio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogError("No se ha asignado el Transform del jugador.");
            return;
        }

        // Calcula la distancia entre el enemigo y el jugador
        float distancia = Vector3.Distance(transform.position, player.position);

        if (!sonidoActivo && distancia <= distanciaActivacion)
        {
            fuenteAudio.Play();
            sonidoActivo = true;
        }
        else if (sonidoActivo && distancia >= distanciaDesactivacion)
        {
            fuenteAudio.Stop();
            sonidoActivo = false;
        }
    }
}