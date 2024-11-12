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

    private float x, y;
    private bool isGrounded;
    private bool hasSword = false;
    private bool isDead = false;
    private int punchToggle = 0;
    public float jumpHeight = 3;
    public float punchSpeed = 1.5f;
    public float fuerzaCaer = 10f;

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

        if (Input.GetKeyDown("q") && hasSword)
            animator.Play("Esquivar/Rodar");

        if (Input.GetKeyDown("e"))
        {
            if (hasSword)
                animator.Play("AtaqueEspada");
            else
                animator.Play(punchToggle == 0 ? "Puño1" : "Puño2");

            punchToggle = 1 - punchToggle;
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
            cogerArmas.ActivarArmar(slotIndex - 2); // Ajusta el índice de arma
        }
        else
        {
            hasSword = false;
            cogerArmas.DesactivarArmas();
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
    }
}
