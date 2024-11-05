using UnityEngine;
using UnityEngine.UI;

public class VidaEnemigo : MonoBehaviour
{
    public float Salud = 100;
    public float SaludMaxima = 100;
    public Image BarraSalud; // Arrastra aquí la imagen de la barra de salud
    public Text TextoSalud; // Arrastra aquí el texto de la salud
    public GameObject muerteParticulasPrefab; // Prefab de partículas

    private Animator animator;
    private PlayerScoreManager scoreManager;

    void Start()
    {
        // Obtén el Animator si está en el mismo objeto o configúralo manualmente en el inspector
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator no encontrado en el objeto enemigo.");
        }
        ActualizarInterfaz();

        // Encuentra el componente PlayerScoreManager en la escena
        scoreManager = FindObjectOfType<PlayerScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("PlayerScoreManager no encontrado en la escena.");
        }
    }

    public void RecibirDaño(float daño)
    {
        Salud -= daño;
        ActualizarInterfaz();

        if (Salud <= 0)
        {
            Salud = 0;
            IniciarMuerte();
        }
    }

    void ActualizarInterfaz()
    {
        BarraSalud.fillAmount = Salud / SaludMaxima;
        TextoSalud.text = "+ " + Salud.ToString("f0");
    }

    private void IniciarMuerte()
    {
        // Instancia las partículas solo en el momento de la muerte
        if (muerteParticulasPrefab != null)
        {
            GameObject particulas = Instantiate(muerteParticulasPrefab, transform.position, Quaternion.identity);
            Destroy(particulas, 3f); // Destruye las partículas después de 3 segundos
        }

        // Llama al método de PlayerScoreManager para actualizar el contador
        if (scoreManager != null)
        {
            if (CompareTag("Enemigo"))
            {
                scoreManager.EnemigoDerrotado();
            }
            else if (CompareTag("Jefe"))
            {
                scoreManager.JefeDerrotado();
            }
        }

        // Destruye el enemigo después de un tiempo
        Destroy(gameObject, 1.5f); // Ajusta el tiempo según la duración de la animación
    }
}
