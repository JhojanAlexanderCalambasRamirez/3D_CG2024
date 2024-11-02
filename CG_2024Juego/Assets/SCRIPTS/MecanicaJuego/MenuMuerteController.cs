using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMuerteController : MonoBehaviour
{
    public GameObject menuMuerte; // Referencia al panel de muerte
    private CanvasGroup canvasGroup;

    void Start()
    {
        // Asegúrate de que el menú de muerte esté desactivado al iniciar
        menuMuerte.SetActive(false);

        canvasGroup = menuMuerte.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = menuMuerte.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void ActivarMenuMuerte()
    {
        Debug.Log("Panel 'Dead' activado");  // Confirmación en la consola

        menuMuerte.SetActive(true);
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        Time.timeScale = 0f;
    }

    public void IrAlMenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void RecargarEscena()
    {
        StartCoroutine(RestaurarTiempoYRecargar(SceneManager.GetActiveScene().name));
    }

    private IEnumerator RestaurarTiempoYRecargar(string escena)
    {
        Time.timeScale = 1f; // Restaurar el tiempo de juego

        // Esperar un frame para asegurar que Time.timeScale se restaura
        yield return null;

        SceneManager.LoadScene(escena);
    }
}
