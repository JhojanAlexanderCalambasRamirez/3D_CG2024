using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public GameObject controlesPanel;

    void Start()
    {
        controlesPanel.SetActive(false);
        Time.timeScale = 1f; // Asegura el tiempo de juego normal al iniciar
    }

    public void LoadScene(string SceneName)
    {
        Time.timeScale = 1f; // Restaurar el tiempo antes de cambiar de escena
        SceneManager.LoadScene(SceneName);
    }

    public void AbrirControles()
    {
        controlesPanel.SetActive(true);
    }

    public void VolverAlMenu()
    {
        controlesPanel.SetActive(false);
    }
}
