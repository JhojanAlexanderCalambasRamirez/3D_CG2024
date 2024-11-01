using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMuerteController : MonoBehaviour
{
    public GameObject menuMuerte; // Referencia al panel de muerte

    void Start()
    {
        // Asegúrate de que el menú de muerte esté desactivado al iniciar y que el tiempo esté normalizado
        menuMuerte.SetActive(false);
        Time.timeScale = 1f; // Restablecer el tiempo de juego al iniciar la escena
    }

    public void ActivarMenuMuerte()
    {
        // Activar el menú de muerte
        menuMuerte.SetActive(true);
        // Pausar el juego
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
        }
    }

    // Función para ir al menú principal
    public void IrAlMenuPrincipal()
    {
        // Restaurar el tiempo de juego antes de cambiar de escena
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }

    // Función para recargar la escena actual
    public void RecargarEscena()
    {
        // Restaurar el tiempo de juego antes de recargar la escena
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
