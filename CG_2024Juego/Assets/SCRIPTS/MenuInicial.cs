using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject controlesPanel;

    void Start()
    {
        // Asegúrate de que el panel de controles esté desactivado al iniciar
        controlesPanel.SetActive(false);
    }

    public void jugar()
    {
        // Cambia a la escena del juego
        SceneManager.LoadScene("SampleScene");
    }

    public void AbrirControles()
    {
        // Abre el panel de controles cuando se presione el botón de controles
        controlesPanel.SetActive(true);
    }

    public void VolverAlMenu()
    {
        // Cierra el panel de controles y vuelve al menú principal
        controlesPanel.SetActive(false);
    }
}
