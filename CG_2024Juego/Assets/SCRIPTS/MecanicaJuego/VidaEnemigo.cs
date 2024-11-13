using UnityEngine;
using UnityEngine.UI;

public class VidaEnemigo : MonoBehaviour
{
    public float Salud = 100;
    public float SaludMaxima = 100;
    public Image BarraSalud;
    public Text TextoSalud;
    public GameObject muerteParticulasPrefab;

    private Animator animator;
    private PlayerScoreManager scoreManager;

    void Start()
    {
        animator = GetComponent<Animator>();
        ActualizarInterfaz();
        scoreManager = FindObjectOfType<PlayerScoreManager>();
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
        if (muerteParticulasPrefab != null)
        {
            GameObject particulas = Instantiate(muerteParticulasPrefab, transform.position, Quaternion.identity);
            Destroy(particulas, 3f);
        }

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

        Destroy(gameObject, 0f);
    }
}
