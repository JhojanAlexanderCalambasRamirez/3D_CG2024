//Alexander Calambas - 2190555
//Juan Manuel Santos - 2215928
//Juan David Rios - 2225674

using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioFinal : MonoBehaviour
{
    private Vector2 puntoInicial;  // Para recordar la posición inicial del objeto

    void Start()
    {
        puntoInicial = transform.position;  // Guardar la posición inicial
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Guardar progreso antes de cambiar a la escena final
            GameManager.instance.SaveProgress();
            SceneManager.LoadScene("EscenaFinal");  // Cambiar a la escena final
        }
    }
}
