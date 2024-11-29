using System.Collections.Generic;
using UnityEngine;

public class ParkourBlocksController : MonoBehaviour
{
    public enum MovimientoTipo { Vertical, Horizontal }
    public List<GameObject> bloques;
    public float intensidadMovimiento = 2f;
    public float velocidadMovimiento = 2f;
    public MovimientoTipo tipoDeMovimiento;
    private List<Vector3> posicionesIniciales = new List<Vector3>();
    private List<Rigidbody> bloquesRigidbodies = new List<Rigidbody>();

    void Start()
    {
        foreach (var bloque in bloques)
        {
            posicionesIniciales.Add(bloque.transform.position);

            // Agrega el Rigidbody si el bloque lo tiene
            Rigidbody rb = bloque.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogWarning($"El bloque {bloque.name} no tiene un Rigidbody.");
                bloquesRigidbodies.Add(null); // Añade un valor nulo si no tiene Rigidbody
            }
            else
            {
                bloquesRigidbodies.Add(rb); // Añade el Rigidbody a la lista
                rb.isKinematic = true; // Asegúrate de que el Rigidbody sea cinemático para usar MovePosition
            }
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < bloques.Count; i++)
        {
            if (bloques[i] != null && bloquesRigidbodies[i] != null)
            {
                Vector3 nuevaPosicion = posicionesIniciales[i];

                if (tipoDeMovimiento == MovimientoTipo.Vertical)
                {
                    // Mover en el eje Y
                    nuevaPosicion.y += Mathf.Sin(Time.time * velocidadMovimiento) * intensidadMovimiento;
                }
                else if (tipoDeMovimiento == MovimientoTipo.Horizontal)
                {
                    // Mover en el eje X
                    nuevaPosicion.x += Mathf.Sin(Time.time * velocidadMovimiento) * intensidadMovimiento;
                }

                // Mueve el bloque usando MovePosition para mantener las colisiones
                bloquesRigidbodies[i].MovePosition(nuevaPosicion);
            }
        }
    }
}
