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

    private float x, y;
    private bool isGrounded;
    private bool hasSword = false;
    private bool isDead = false;
    private int punchToggle = 0;
    public float jumpHeight = 3;
    public float punchSpeed = 1.5f; // Velocidad de la animación de puño

    void Update()
    {
        if (isDead) return;

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, y * Time.deltaTime * runSpeed);

        // Cambia la animación de movimiento en función de si tiene una espada o no
        bool isMoving = x != 0 || y != 0;
        if (hasSword)
        {
            animator.SetBool("IsMovingWithSword", isMoving);
        }
        else
        {
            animator.SetBool("Other", isMoving);
            animator.SetFloat("VelX", x);
            animator.SetFloat("VelY", y);
        }

        // Verifica si está en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (Input.GetKey("space") && isGrounded && !isDead)
        {
            animator.Play("Jump");
            Invoke("Jump", 0.1f);
        }

        // Rodar/esquivar solo si tiene espada
        if (Input.GetKeyDown("q") && hasSword)
        {
            animator.Play("Esquivar/Rodar");
        }

        // Ataques con y sin espada
        if (Input.GetKeyDown("e"))
        {
            if (hasSword)
            {
                animator.Play("AtaqueEspada");
            }
            else
            {
                animator.Play(punchToggle == 0 ? "Puño1" : "Puño2");
                punchToggle = 1 - punchToggle;
            }
        }

        // Simulación de daño para pruebas
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            hasSword = true; // Activa la espada
            animator.Play("EquiparEspada"); // Reproduce la animación de equipar espada
            Destroy(other.gameObject); // Destruye la espada en la escena después de recogerla
        }
    }
}
