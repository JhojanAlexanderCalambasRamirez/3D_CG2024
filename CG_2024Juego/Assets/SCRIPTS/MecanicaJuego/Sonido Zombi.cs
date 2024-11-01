using UnityEngine;

public class SonidoZombi : MonoBehaviour
{
    public AudioSource fuenteAudio;
    public Transform player;  // Asigna el Transform del jugador en el Inspector
    public float distanciaActivacion = 0.05f; // 5 cm
    public float distanciaDesactivacion = 0.08f; // 8 cm
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
