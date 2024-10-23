using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float runSpeed = 7f;
    public float rotationSpeed = 250f;
    public float jumpHeight = 3f;

    public Transform groundCheck;    // Objeto que verifica si el personaje est� tocando el suelo
    public float groundDistance = 0.1f;  // Distancia para verificar si est� tocando el suelo
    public LayerMask groundMask;     // M�scara para identificar qu� objetos son considerados como "suelo"

    private float x, y;
    private bool isGrounded;  // Verifica si el personaje est� en el suelo

    void Update()
    {
        // Obtener inputs del teclado
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Controlar la rotaci�n del personaje
        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);

        // Mover el personaje hacia adelante o atr�s
        transform.Translate(0, 0, y * Time.deltaTime * runSpeed);

        // Animar movimiento del personaje
        GetComponent<Animator>().SetFloat("VelX", x);
        GetComponent<Animator>().SetFloat("VelY", y);

        // Si el jugador presiona "R", activar la animaci�n de baile
        if (Input.GetKey("r"))
        {
            GetComponent<Animator>().SetBool("Other", false);
            GetComponent<Animator>().Play("Dance");
        }

        // Activar animaci�n de movimiento si el personaje se est� moviendo
        if (x != 0 || y != 0)
        {
            GetComponent<Animator>().SetBool("Other", true);
        }

        // Verificar si el personaje est� tocando el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Ejecutar el salto si se presiona la tecla espacio y el personaje est� en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Animator>().Play("Jump");
            Invoke("PerformJump", 0.1f);
        }
    }

    // M�todo para el salto
    public void PerformJump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
}
