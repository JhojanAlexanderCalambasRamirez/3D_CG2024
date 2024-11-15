using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMuerteController : MonoBehaviour
{
    public GameObject panelDead;
    private CanvasGroup canvasGroupCanvas;  // Controla el canvas principal
    private CanvasGroup canvasGroupDead;    // Controla el panel de muerte

    void Start()
    {
        // Obtén o añade el CanvasGroup al canvas principal
        canvasGroupCanvas = GetComponent<CanvasGroup>();
        if (canvasGroupCanvas == null)
        {
            canvasGroupCanvas = gameObject.AddComponent<CanvasGroup>();
        }

        // Obtén o añade el CanvasGroup al panel de muerte
        if (panelDead != null)
        {
            canvasGroupDead = panelDead.GetComponent<CanvasGroup>();
            if (canvasGroupDead == null)
            {
                canvasGroupDead = panelDead.AddComponent<CanvasGroup>();
            }
            panelDead.SetActive(false); // Asegurarse de que esté desactivado al inicio
        }
    }

    public void ActivarMenuMuerte()
    {
        // Desactivar la interacción en el canvas principal (todo menos el panel "Dead")
        canvasGroupCanvas.interactable = false;
        canvasGroupCanvas.blocksRaycasts = false;

        // Activar el panel de muerte y permitir su interacción
        if (panelDead != null)
        {
            panelDead.SetActive(true);
            canvasGroupDead.interactable = true;
            canvasGroupDead.blocksRaycasts = true;
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
