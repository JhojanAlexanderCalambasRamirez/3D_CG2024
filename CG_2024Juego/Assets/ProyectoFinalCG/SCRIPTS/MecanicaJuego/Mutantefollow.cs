using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float activationRange = 10.0f; // Distancia para activar al enemigo
    public float speed = 2.0f;            // Velocidad de movimiento del enemigo
    public float rotationSpeed = 5.0f;    // Velocidad de rotación para seguir al jugador
    public float attackRange = 2.0f;      // Distancia a la que el enemigo atacará al jugador
    public float daño = 10.0f;            // Cantidad de daño que inflige al jugador
    public float tiempoEntreAtaques = 1.0f; // Tiempo de espera entre ataques

    private Transform player;
    private Animator animator;
    private bool isActivated = false;     // Indica si el enemigo ha sido activado
    private float tiempoProximoAtaque = 0f;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player no encontrado en la escena.");
        }

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (!isActivated && distanceToPlayer <= activationRange)
            {
                isActivated = true;
                animator.ResetTrigger("Idle");
            }

            if (isActivated)
            {
                if (distanceToPlayer <= attackRange)
                {
                    animator.SetTrigger("AtacarM");
                    animator.SetBool("isRunning", false);

                    // Verifica si ya ha pasado el tiempo para un nuevo ataque
                    if (Time.time >= tiempoProximoAtaque)
                    {
                        // Llama al método para hacer daño al jugador
                        HacerDañoAlJugador();
                        tiempoProximoAtaque = Time.time + tiempoEntreAtaques; // Actualiza el tiempo para el siguiente ataque
                    }
                }
                else
                {
                    animator.SetBool("isRunning", true);
                    Vector3 direction = (player.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;

                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
    }

    void HacerDañoAlJugador()
    {
        Vida vidaJugador = player.GetComponent<Vida>();
        if (vidaJugador != null)
        {
            vidaJugador.RecibirDaño(daño);
        }
        else
        {
            Debug.LogWarning("El jugador no tiene el componente VidaJugador.");
        }
    }
}
