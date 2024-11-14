

using UnityEngine;
using TMPro;  // Importamos el namespace de TextMeshPro
/**
* Clase que gestiona la carga del nombre del jugador desde PlayerPrefs y lo asigna a un objeto de texto en la interfaz.
* Utiliza TextMeshPro para mostrar el nombre guardado previamente.
* @author Alexander Calambas - 2190555
* @author Juan Manuel Santos - 2215928
* @author Juan David Rios - 2225674
* @date 11 noviembre 2024
* @version 1.0
*/
public class CargaName : MonoBehaviour
{
    private TMP_Text nombreText;  // Cambiamos a TMP_Text para TextMeshPro

    /**
   * Método llamado al inicio del juego. Busca el objeto con la etiqueta "nombre1" y carga el nombre almacenado en PlayerPrefs.
   * Si no se encuentra el objeto, se muestra un error en la consola.
   * @return Ninguno
   */
    private void Start()  // Corrige la S mayúscula en Start
    {
        // Buscamos el objeto por su tag y obtenemos el componente TMP_Text
        GameObject nombre1 = GameObject.FindGameObjectWithTag("nombre1");

        if (nombre1 != null)
        {
            // Obtenemos el componente TMP_Text y asignamos el texto guardado en PlayerPrefs
            nombreText = nombre1.GetComponent<TMP_Text>();
            nombreText.text = PlayerPrefs.GetString("nombre1", "Rojo"); // Usamos la clave "nombre1"
        }
        else
        {
            Debug.LogError("El objeto con la etiqueta 'nombre1' no se encontró.");
        }
    }
}
