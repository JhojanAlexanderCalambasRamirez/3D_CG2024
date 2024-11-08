using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorazonRojo : MonoBehaviour
{
    public float CantidadCura;
    public float velocidadGiro = 100f;

    private void Update()
    {

        transform.Rotate(0, 0, velocidadGiro * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1")&& other.GetComponent<Vida>())
        {
            other.GetComponent<Vida>().RecibirCura(CantidadCura);

            Destroy(gameObject);        
        }
        
    }
}
