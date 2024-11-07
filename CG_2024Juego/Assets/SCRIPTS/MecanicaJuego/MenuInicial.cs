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
        // Verifica si está en modo editor
        if (!Application.isPlaying) return;

        if (Time.timeScale != 1f)
        {
            Time.timeScale = 1f;
        }

        if (sceneName == "Bosque-Limitado" || SceneManager.GetActiveScene().name != sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        if (!Application.isPlaying) return;

        if (Time.timeScale != 1f)
        {
            Time.timeScale = 1f;
        }

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
