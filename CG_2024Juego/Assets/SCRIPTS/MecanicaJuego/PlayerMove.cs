using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Button[] missionButtons;       // Botones de las misiones
    public Slider[] missionSliders;       // Sliders de progreso de las misiones
    public GameObject[] missionTexts;     // Textos con el contexto de cada misión

    private bool[] missionCompleted;      // Estado de cada misión
    private bool missionActive = false;   // Para verificar si una misión está en progreso
    private int activeMissionIndex = -1;  // Índice de la misión activa

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

    void Start()
    {
        missionCompleted = new bool[missionButtons.Length];

        // Configura los botones de misión
        for (int i = 0; i < missionButtons.Length; i++)
        {
            int index = i; // Necesario para el contexto de la lambda
            missionButtons[i].onClick.AddListener(() => StartMission(index));
        }
    }
        
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
        int daño = hasSword && selectedWeaponIndex >= 0 ? cogerArmas.ObtenerDañoArma(selectedWeaponIndex) : cogerArmas.ObtenerDañoPuño();

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
        if (missionActive)
        {
            if (other.CompareTag("Sword") && activeMissionIndex == 0 && other.name == "Sword_Basica")
            {
                CompleteMission();
            }
            else if (other.CompareTag("Sword") && activeMissionIndex == 1 && other.name == "Sword_Red")
            {
                CompleteMission();
            }
            else if (other.CompareTag("Sword") && activeMissionIndex == 2 && other.name == "Sword_Green")
            {
                CompleteMission();
            }
            else if (other.CompareTag("Sword") && activeMissionIndex == 3 && other.name == "Sword_Blue")
            {
                CompleteMission();
            }
        }

        if (other.CompareTag("Enemigo"))
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

    private void StartMission(int index)
    {
        if (missionCompleted[index] || missionActive) return;

        activeMissionIndex = index;
        missionActive = true;

        for (int i = 0; i < missionButtons.Length; i++)
        {
            if (i != index) missionButtons[i].interactable = false;
        }

        missionSliders[index].value = 0;
        missionSliders[index].fillRect.GetComponentInChildren<Image>().color = Color.red;
    }

    private void CompleteMission()
    {
        missionCompleted[activeMissionIndex] = true;
        missionSliders[activeMissionIndex].value = 1;
        missionSliders[activeMissionIndex].fillRect.GetComponentInChildren<Image>().color = Color.green;

        missionActive = false;
        activeMissionIndex = -1;

        foreach (Button button in missionButtons)
        {
            button.interactable = true;
        }
    }
}
