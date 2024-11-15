using UnityEngine;

public class CarouselBreathing : MonoBehaviour
{
    public float scaleSpeed = 1f;      // Velocidad del cambio de tamaño
    public float scaleAmount = 0.1f;   // Cantidad de cambio en el tamaño

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale; // Guarda el tamaño inicial
    }

    void Update()
    {
        // Calcula un nuevo tamaño usando una oscilación senoidal
        float scaleFactor = 1 + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = initialScale * scaleFactor;
    }
}
