using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControladorDatosJuego : MonoBehaviour
{
    public GameObject jugador;
    public string archivoDeGuardado;
    public DatosJuego datosJuego = new DatosJuego();

    private void Awake()
    {
        // Usamos persistentDataPath en lugar de dataPath para guardar archivos
        archivoDeGuardado = Application.persistentDataPath + "/datosJuego.json";
        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // Cargar datos cuando se presiona la tecla 'C'
        if (Input.GetKeyDown(KeyCode.C))
        {
            CargarDatos();
        }

        // Guardar datos cuando se presiona la tecla 'G'
        if (Input.GetKeyDown(KeyCode.G))
        {
            GuardarDatos();
        }
    }

    private void CargarDatos()
    {
        // Verificamos si el archivo existe antes de intentar leerlo
        if (File.Exists(archivoDeGuardado))
        {
            // Leemos el contenido del archivo JSON
            string contenido = File.ReadAllText(archivoDeGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);

            // Actualizamos la posición del jugador con los datos cargados
            jugador.transform.position = datosJuego.posicion;

            // Actualizamos el nombre del jugador, el puntaje y los ítems
            PlayerPrefs.SetString("nombre1", datosJuego.nombreJugador);
            FindObjectOfType<GameManager>().AddPoints(datosJuego.puntaje - FindObjectOfType<GameManager>().score);  // Ajusta el puntaje
            FindObjectOfType<GameManager>().AddItem(datosJuego.items - FindObjectOfType<GameManager>().items);  // Ajusta los ítems

            Debug.Log("Datos cargados correctamente. Posición del jugador: " + datosJuego.posicion);
        }
        else
        {
            Debug.Log("El archivo de guardado no existe.");
        }
    }


private void GuardarDatos()
    {
        // Creamos un nuevo objeto DatosJuego para guardar la posición actual del jugador y otros datos
        DatosJuego nuevosDatos = new DatosJuego()
        {
            posicion = jugador.transform.position,
            nombreJugador = PlayerPrefs.GetString("nombre1", "Rojo"),  // Cargar el nombre del jugador de PlayerPrefs
            puntaje = FindObjectOfType<GameManager>().score,  // Obtener el puntaje actual
            items = FindObjectOfType<GameManager>().items  // Obtener la cantidad de ítems
        };

        // Convertimos los datos a formato JSON
        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);

        // Guardamos el JSON en el archivo
        File.WriteAllText(archivoDeGuardado, cadenaJSON);

        Debug.Log("Datos guardados correctamente.");
    }

}

public class DatosJuego
{
    public Vector3 posicion;  // Guardamos la posición del jugador
    public string nombreJugador;  // Nombre del jugador
    public int puntaje;  // Puntaje del jugador
    public int items;  // Cantidad de ítems
}

