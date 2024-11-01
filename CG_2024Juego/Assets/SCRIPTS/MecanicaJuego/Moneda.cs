//Alexander Calambas - 2190555
//Juan Manuel Santos - 2215928
//Juan David Rios - 2225674

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{

    [SerializeField] private float cantidadPuntos;

    private void OnTriggerEnter2D(Collider2D other)
    {
    if (other.CompareTag("Player"))
        {
            ControladorPuntos.Instance.SumarPuntos(cantidadPuntos);
            Destroy(gameObject);
        }
    }
}