using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Variables para los datos que deseas mantener entre escenas
    public int vidaJugador = 100;
    public int contadorColisiones = 0;
    public int puntosAcumulados = 0;
    public List<string> misionesCompletadas = new List<string>();
    public bool[] armasRecogidas = new bool[4];
    public int contadorEnemigos;
    public int jefesMatados;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Evita que se destruya entre escenas
        }
    }
    public void CompletarMision(string nombreMision)
    {
        // Implementación del método para completar una misión
        Debug.Log("Misión completada: " + nombreMision);
        // Aquí puedes añadir la lógica para completar la misión
    }
}
