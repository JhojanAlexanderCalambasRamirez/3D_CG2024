using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float runSpeed = 7;
    public float rotationSpeed = 250;
    public Animator animator;
    public Rigidbody rb;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    public Vida vida;
    public CogerArmas cogerArmas;
    public int dañoPuño = 10;

    private float x, y;
    private bool isGrounded;
    private bool hasSword = false;
    private bool isDead = false;
    private int punchToggle = 0;
    private bool isAttacking = false;
    private VidaEnemigo enemigoActual;   // Referencia al enemigo actual
    public float jumpHeight = 3;
    public float punchSpeed = 1.5f;
    public float fuerzaCaer = 10f;
    private int selectedWeaponIndex = -1;

    void Update()
    {
        if (isDead) return;

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, y * Time.deltaTime * runSpeed);

        bool isMoving = x != 0 || y != 0;
        animator.SetBool("IsMovingWithSword", hasSword && isMoving);
        animator.SetBool("Other", !hasSword && isMoving);
        animator.SetFloat("VelX", x);
        animator.SetFloat("VelY", y);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && rb.velocity.y <= 0)
            animator.SetBool("IsFalling", false);

        if (Input.GetKey("space") && isGrounded && !isDead)
        {
            animator.Play("Jump");
            Invoke("Jump", 0.1f);
        }

        if (!isGrounded && rb.velocity.y < 0)
        {
            animator.SetBool("IsFalling", true);
            rb.AddForce(Vector3.down * fuerzaCaer, ForceMode.Acceleration);
        }

        // Animación de ataque sin necesidad de estar cerca de un enemigo
        if (Input.GetKeyDown("e") && !isAttacking)
        {
            isAttacking = true;
            if (hasSword)
            {
                animator.Play("AtaqueEspada");
                AtacarConEspada();
            }
            else
            {
                animator.Play(punchToggle == 0 ? "Puño1" : "Puño2");
                if (enemigoActual != null)
                {
                    enemigoActual.RecibirDaño(dañoPuño);
                    Debug.Log("Ataque con puño: " + dañoPuño + " de daño aplicado al enemigo.");
                }
            }

            punchToggle = 1 - punchToggle;
        }

        // Restablecer el estado de ataque si la animación ha terminado
        if (isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsName("AtaqueEspada") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Puño1") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Puño2"))
        {
            isAttacking = false;
            
        }

        if (Input.GetKeyDown("k"))
        {
            vida.RecibirDaño(vida.Salud, false);
            isDead = true;
        }
        else if (Input.GetKeyDown("l"))
        {
            vida.RecibirDaño(vida.Salud, true);
            isDead = true;
        }
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

    public void OnInventorySlotChanged(int slotIndex)
    {
        if (slotIndex >= 2 && slotIndex < 6)
        {
            hasSword = true;
            selectedWeaponIndex = slotIndex - 2;
            cogerArmas.ActivarArmar(selectedWeaponIndex);
        }
        else
        {
            hasSword = false;
            selectedWeaponIndex = -1;
            cogerArmas.DesactivarArmas();
        }
    }

    private void AtacarConEspada()
    {
        int daño = hasSword && selectedWeaponIndex >= 0 ?
            cogerArmas.ObtenerDañoArma(selectedWeaponIndex) :
            cogerArmas.ObtenerDañoPuño();

        if (enemigoActual != null)
        {
            enemigoActual.RecibirDaño(daño);
            Debug.Log("Ataque con " + (hasSword ? "espada" : "puño") + ": " + daño + " de daño aplicado");
        }
        else
        {
            Debug.Log("No se encontró enemigo en rango para atacar.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            hasSword = true;
            animator.Play("EquiparEspada");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemigo"))
        {
            enemigoActual = other.GetComponent<VidaEnemigo>();
            Debug.Log("En rango de ataque con el enemigo.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            enemigoActual = null;
            Debug.Log("Fuera de rango de ataque con el enemigo.");
        }
    }
}
