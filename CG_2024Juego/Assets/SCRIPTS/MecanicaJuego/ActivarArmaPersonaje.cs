using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarArmaPersonaje : MonoBehaviour
{
    public CogerArmas cogerArmas;
    public int numeroArma;
    // Start is called before the first frame update
    void Start()
    {
        cogerArmas = GameObject.FindGameObjectWithTag("Player1").GetComponent<CogerArmas>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1")
        {
            cogerArmas.ActivarArmar(numeroArma);
            Destroy(gameObject);
        }
    }
}