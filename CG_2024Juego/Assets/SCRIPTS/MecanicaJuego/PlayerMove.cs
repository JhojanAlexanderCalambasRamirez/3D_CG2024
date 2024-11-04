using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float runSpeed = 7;
    public float rotationSpeed = 250;

    public Animator animator;
    private float x, y;

    public Rigidbody rb;
    public float jumpHeight = 3;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    bool isGrounded;
    bool hasSword = false;
    bool isDead = false;
    int punchToggle = 0;

    // Referencia a la clase Vida
    public Vida vida;

    void Update()
    {
        if (isDead) return;

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, y * Time.deltaTime * runSpeed);

        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);

        if (Input.GetKey("f"))
        {
            animator.SetBool("Other", false);
            animator.Play("Dance");
        }
        if (x != 0 || y != 0)
        {
            animator.SetBool("Other", true);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (Input.GetKey("space") && isGrounded && !isDead)
        {
            animator.Play("Jump");
            Invoke("Jump", 0.1f);
        }

        if (Input.GetKeyDown("q") && hasSword)
        {
            animator.Play("Esquivar/Rodar");
        }

        if (Input.GetKeyDown("e") && !hasSword)
        {
            animator.Play(punchToggle == 0 ? "Puño1" : "Puño2");
            punchToggle = 1 - punchToggle;
        }

        if (Input.GetKeyDown("e") && hasSword)
        {
            animator.Play("AtaqueEspada");
        }

        if (Input.GetKeyDown("1"))
        {
            hasSword = true;
            animator.Play("EquiparEspada");
        }

        if (Input.GetKeyDown("2") && hasSword)
        {
            hasSword = false;
            animator.Play("GuardarEspada");
        }

        // Ejemplo de daño por enemigo y jefe (reemplaza con condiciones reales)
        if (Input.GetKeyDown("k"))  // Daño de enemigo
        {
            vida.RecibirDaño(vida.Salud, false); // Muerte por enemigo
            isDead = true;
        }
        else if (Input.GetKeyDown("l"))  // Daño de jefe
        {
            vida.RecibirDaño(vida.Salud, true); // Muerte por jefe
            isDead = true;
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }
}
