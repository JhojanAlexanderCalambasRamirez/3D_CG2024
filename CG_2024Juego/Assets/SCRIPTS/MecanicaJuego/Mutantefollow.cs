using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NewBehaviourScript : MonoBehaviour
{
    public float activationRange = 10.0f; // Distancia para activar al enemigo
    public float speed = 2.0f;            // Velocidad de movimiento del enemigo
    public float rotationSpeed = 5.0f;    // Velocidad de rotación para seguir al jugador
    public float attackRange = 2.0f;      // Distancia a la que el enemigo atacará al jugador

    private Transform player;
    private Animator animator;
    private bool isActivated = false;     // Indica si el enemigo ha sido activado

    void Start()
    {
        // Encuentra al jugador por su tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player no encontrado en la escena.");
        }

        // Obtén el componente Animator del enemigo
        animator = GetComponent<Animator>();


    }

    void Update()
    {
        if (player != null)
        {
            // Calcula la distancia entre el enemigo y el jugador
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Activa el enemigo si el jugador está dentro del rango de activación
            if (!isActivated && distanceToPlayer <= activationRange)
            {
                isActivated = true; // Marca el enemigo como activado
                animator.ResetTrigger("Idle"); // Quita la animación de Idle
            }

            // Si el enemigo ha sido activado, sigue la lógica de movimiento y ataque
            if (isActivated)
            {
                if (distanceToPlayer <= attackRange)
                {
                    // En rango de ataque
                    animator.SetTrigger("AtacarM");
                    animator.SetBool("isRunning", false); // Detiene el correr
                }
                else
                {
                    // Fuera del rango de ataque, pero dentro del rango de seguimiento
                    animator.SetBool("isRunning", true);

                    // Movimiento y rotación hacia el jugador
                    Vector3 direction = (player.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;

                    // Rotación hacia el jugador
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                // Mantener la animación en Idle mientras el enemigo no esté activado
                
                animator.SetBool("isRunning", false);
            }
        }
    }
}