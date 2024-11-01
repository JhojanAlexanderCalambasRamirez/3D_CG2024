//Alexander Calambas - 2190555
//Juan Manuel Santos - 2215928
//Juan David Rios - 2225674


using UnityEngine;
using TMPro;  // Importamos el namespace de TextMeshPro

public class CargaName : MonoBehaviour
{
    private TMP_Text nombreText;  // Cambiamos a TMP_Text para TextMeshPro

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
