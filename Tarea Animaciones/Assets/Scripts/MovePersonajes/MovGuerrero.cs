using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float runSpeed = 7f;
    public float rotationSpeed = 250f;
    public float jumpHeight = 3f;

    public Transform groundCheck;    // Objeto que verifica si el personaje está tocando el suelo
    public float groundDistance = 0.1f;  // Distancia para verificar si está tocando el suelo
    public LayerMask groundMask;     // Máscara para identificar qué objetos son considerados como "suelo"

    private float x, y;
    private bool isGrounded;  // Verifica si el personaje está en el suelo

    void Update()
    {
        // Obtener inputs del teclado
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Controlar la rotación del personaje
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);

        // Mover el personaje hacia adelante o atrás
        transform.Translate(0, 0, y * Time.deltaTime * runSpeed);

        // Animar movimiento del personaje
        GetComponent<Animator>().SetFloat("VelX", x);
        GetComponent<Animator>().SetFloat("VelY", y);

        // Si el jugador presiona "R", activar la animación de baile
        if (Input.GetKey("r"))
        {
            GetComponent<Animator>().SetBool("Other", false);
            GetComponent<Animator>().Play("Dance");
        }

        // Activar animación de movimiento si el personaje se está moviendo
        if (x != 0 || y != 0)
        {
            GetComponent<Animator>().SetBool("Other", true);
        }

        // Verificar si el personaje está tocando el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Ejecutar el salto si se presiona la tecla espacio y el personaje está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Animator>().Play("Jump");
            Invoke("PerformJump", 0.1f);
        }
    }

    // Método para el salto
    public void PerformJump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
}
