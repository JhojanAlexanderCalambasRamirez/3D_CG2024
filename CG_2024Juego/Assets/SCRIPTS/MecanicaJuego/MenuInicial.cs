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

    public void LoadScene(string SceneName)
    {
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
