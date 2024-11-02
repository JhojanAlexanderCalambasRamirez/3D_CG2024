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
    }

    // Método para cargar la escena usando el nombre
    public void LoadSceneByName(string sceneName)
    {
        if (Time.timeScale != 1f)
        {
            Time.timeScale = 1f;
        }

        // Verifica si la escena solicitada coincide con "Bosque-Limitado"
        if (sceneName == "Bosque-Limitado" || SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    // Método para cargar la escena usando el índice en Build Settings
    public void LoadSceneByIndex(int sceneIndex)
    {
        if (Time.timeScale != 1f)
        {
            Time.timeScale = 1f;
        }

        // Verifica si el índice coincide con el de "Bosque-Limitado"
        if (sceneIndex == 2 || SceneManager.GetActiveScene().buildIndex != sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
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
