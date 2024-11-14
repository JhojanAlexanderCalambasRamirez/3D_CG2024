using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMuerteController : MonoBehaviour
{
    public GameObject panelDead;

    void Start()
    {
        panelDead.SetActive(false);
    }

    public void ActivarMenuMuerte()
    {
        panelDead.SetActive(true);
        Time.timeScale = 1f;
    }

    public void RecargarEscena()
    {
        // Restablecer los datos del jugador en GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarDatosJugador();
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenuPrincipal()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarDatosJugador();
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }

}
