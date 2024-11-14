using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMuerteController : MonoBehaviour
{
    public GameObject panelDead; // Panel completo de "Dead"
    public GameObject menuMuerte; // Objeto que contiene los botones de muerte dentro de "Dead"

    void Start()
    {
        // Asegurarse de que el menú de muerte esté desactivado al iniciar
        panelDead.SetActive(false);
    }

    public void ActivarMenuMuerte()
    {
        Debug.Log("Panel 'Dead' activado"); // Confirmación en la consola
        panelDead.SetActive(true); // Mostrar el panel Dead completo
        Time.timeScale = 0f; // Pausar el juego
    }

    public void IrAlMenuPrincipal()
    {
        Debug.Log("Volviendo al menú principal"); // Confirmación en la consola
        Time.timeScale = 1f; // Restaurar el tiempo de juego antes de cambiar de escena
        SceneManager.LoadScene("MenuPrincipal", LoadSceneMode.Single);
    }

    public void RecargarEscena()
    {
        Debug.Log("Recargando la escena actual"); // Confirmación en la consola
        Time.timeScale = 1f; // Restaurar el tiempo de juego antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
