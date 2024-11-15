using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMuerteController : MonoBehaviour
{
    public GameObject panelDead;
    private CanvasGroup canvasGroupDead;

    void Start()
    {
        // Asegurarse de que el panel de muerte esté inicialmente desactivado
        if (panelDead != null)
        {
            panelDead.SetActive(false);

            // Obtener o añadir el CanvasGroup al panel de muerte
            canvasGroupDead = panelDead.GetComponent<CanvasGroup>();
            if (canvasGroupDead == null)
            {
                canvasGroupDead = panelDead.AddComponent<CanvasGroup>();
            }

            // Configuración inicial del CanvasGroup para que esté inactivo
            canvasGroupDead.alpha = 0f;
            canvasGroupDead.interactable = false;
            canvasGroupDead.blocksRaycasts = false;
        }
    }

    public void ActivarMenuMuerte()
    {
        if (panelDead != null)
        {
            // Hacer visible e interactivo el panel de muerte
            panelDead.SetActive(true);
            canvasGroupDead.alpha = 1f;           // Asegura que el panel sea completamente visible
            canvasGroupDead.interactable = true;  // Permitir la interacción con los botones
            canvasGroupDead.blocksRaycasts = true; // Bloquear interacciones con otros elementos
        }

        Time.timeScale = 0f; // Pausar el juego
    }

    public void RecargarEscena()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarDatosJugador();
        }

        Time.timeScale = 1f; // Restaurar el tiempo del juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenuPrincipal()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarDatosJugador();
        }

        Time.timeScale = 1f; // Restaurar el tiempo del juego
        SceneManager.LoadScene("MenuPrincipal");
    }
}
