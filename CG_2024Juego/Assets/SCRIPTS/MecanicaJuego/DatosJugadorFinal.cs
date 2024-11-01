//Alexander Calambas - 2190555
//Juan Manuel Santos - 2215928
//Juan David Rios - 2225674

using UnityEngine;
using TMPro;
using System.IO;  // Para manejar archivos
using System;    // Para manejo de excepciones

public class DatosJugadorFinal : MonoBehaviour
{
    public TMP_Text nombreText;  // Referencia al TextMeshPro para el nombre
    public TMP_Text puntosText;  // Referencia al TextMeshPro para los puntos
    public TMP_Text itemsText;   // Referencia al TextMeshPro para los ítems

    private void Start()
    {
        // Cargar el nombre del jugador desde PlayerPrefs
        string nombreJugador = PlayerPrefs.GetString("nombre1", "Sin nombre");
        nombreText.text = nombreJugador;

        // Cargar los puntos totales acumulados desde PlayerPrefs
        int puntosTotales = PlayerPrefs.GetInt("totalPoints", 0);
        puntosText.text = "Puntos Totales: " + puntosTotales;

        // Cargar los ítems totales acumulados desde PlayerPrefs
        int itemsTotales = PlayerPrefs.GetInt("totalItems", 0);
        itemsText.text = "Ítems Totales: " + itemsTotales;

        // Crear una instancia de DatosJugador con la información actual
        DatosJugador datosJugador = new DatosJugador(nombreJugador, puntosTotales, itemsTotales);

        // Guardar los datos en un archivo JSON
        GuardarDatosEnJSON(datosJugador);
    }

    // Método para guardar los datos en un archivo JSON
    private void GuardarDatosEnJSON(DatosJugador datos)
    {
        // Convertir los datos a formato JSON
        string json = JsonUtility.ToJson(datos, true);  // true para hacer el JSON legible

        // Definir la ruta del archivo
        string rutaArchivo = Application.persistentDataPath + "/datosJugadorFinal.json";

        try
        {
            // Guardar el archivo en la ruta especificada
            File.WriteAllText(rutaArchivo, json);

            // Mensaje para depuración
            Debug.Log("Datos guardados en JSON correctamente: " + rutaArchivo);
        }
        catch (Exception e)
        {
            Debug.LogError("Error al guardar los datos en JSON: " + e.Message);
        }
    }
}
