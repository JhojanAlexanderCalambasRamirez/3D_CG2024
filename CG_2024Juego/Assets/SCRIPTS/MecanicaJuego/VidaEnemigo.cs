using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VidaEnemigo : MonoBehaviour
{
    public float Salud = 100;
    public float SaludMaxima = 100;
    public Image BarraSalud; // Arrastra aquí la imagen de la barra de salud
    public Text TextoSalud; // Arrastra aquí el texto de la salud

    private Animator animator;

    void Start()
    {
        // Obtén el Animator si está en el mismo objeto o configúralo manualmente en el inspector
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator no encontrado en el objeto enemigo.");
        }
        ActualizarInterfaz();
    }

    public void RecibirDaño(float daño)
    {
        Salud -= daño;
        ActualizarInterfaz();

        if (Salud <= 0)
        {
            Salud = 0;
            animator.Play("EnemigoMuere"); // Reemplaza con el nombre de la animación de muerte
            DestruirEnemigo();
        }
    }

    void ActualizarInterfaz()
    {
        BarraSalud.fillAmount = Salud / SaludMaxima;
        TextoSalud.text = "+ " + Salud.ToString("f0");
    }

    private void DestruirEnemigo()
    {
        // Espera un poco antes de destruir al enemigo para ver la animación
        Destroy(gameObject, 1.5f); // Ajusta el tiempo según la duración de la animación
    }
}
