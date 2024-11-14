using UnityEngine;

/**
* Clase que aplica un efecto de respiración (cambio de tamaño) a un objeto.
* El objeto aumenta y disminuye su tamaño de manera oscilante usando una función senoidal.
* @author Alexander Calambas - 2190555
* @author Juan Manuel Santos - 2215928
* @author Juan David Rios - 2225674
* @date 11 noviembre 2024
* @version 1.0
*/
public class CarouselBreathing : MonoBehaviour
{

    public float scaleSpeed = 1f;      // Velocidad del cambio de tamaño
    public float scaleAmount = 0.1f;   // Cantidad de cambio en el tamaño

    private Vector3 initialScale;
    /**
   * Método llamado al inicio del juego. Guarda el tamaño inicial del objeto.
   * @return Ninguno
   */
    void Start()
    {
        initialScale = transform.localScale; // Guarda el tamaño inicial
    }

    /**
    * Método que se llama en cada cuadro. Calcula un nuevo tamaño basado en una oscilación senoidal 
    * y aplica el cambio al objeto.
    * @return Ninguno
    */
    void Update()
    {
        // Calcula un nuevo tamaño usando una oscilación senoidal
        float scaleFactor = 1 + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = initialScale * scaleFactor;
    }
}
