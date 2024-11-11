using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorazonRojo : MonoBehaviour
{
    public float CantidadCura;
    public float velocidadGiro = 100f;
    public ParticleSystem curaEffect; 

    private void Update()
    {
        transform.Rotate(0, 0, velocidadGiro * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") && other.GetComponent<Vida>())
        {
            other.GetComponent<Vida>().RecibirCura(CantidadCura);

            if (curaEffect != null)
            {
                
                Instantiate(curaEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
