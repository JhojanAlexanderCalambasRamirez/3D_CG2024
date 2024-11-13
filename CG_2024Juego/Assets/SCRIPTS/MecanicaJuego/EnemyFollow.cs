using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{


    public float speed = 2.0f;               // Velocidad de movimiento del enemigo
    public float rotationSpeed = 5.0f;       // Velocidad de rotación para seguir al jugador
    public float attackRange = 2.0f;         // Distancia a la que el enemigo atacará al jugador
    public float daño = 10f;                 // Cantidad de daño que el enemigo hace al jugador
    public float tiempoEntreAtaques = 1.0f;  // Tiempo en segundos entre ataques
    public float activationRange = 8.0f;    // Rango en el que el enemigo se activa
    public float deactivationRange = 15.0f;  // Rango en el que el enemigo se desactiva
    

    private Transform player;
    private Vida vidaJugador;
    private Animator animator;
    private float tiempoDesdeUltimoAtaque;
    private bool isActivated = false;

    void Start()
    {
        // Encuentra al jugador por su tag y obtiene el componente Vida
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
        if (playerObj != null)
        {
            player = playerObj.transform;
            vidaJugador = playerObj.GetComponent<Vida>();

            if (vidaJugador == null)
            {
                Debug.LogError("El objeto del jugador no tiene un componente 'Vida'.");
            }
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto con el tag 'Player1'.");
        }

        // Obtén el componente Animator del enemigo
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontró el componente Animator en el enemigo.");
        }

        // Configura la animación inicial en Idle
        animator.SetTrigger("Idle");
    }

    void Update()
    {
        if (player != null && vidaJugador != null && animator != null)
        {
            // Calcula la distancia entre el enemigo y el jugador
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                // Activa el enemigo si el jugador está dentro del rango de activación
                if (!isActivated && distanceToPlayer <= activationRange)
                {
                    isActivated = true; // Marca el enemigo como activado
                    animator.ResetTrigger("Idle"); // Quita la animación de Idle

                }
                // Desactiva el enemigo si el jugador está fuera del rango de desactivación
                else if (isActivated && distanceToPlayer > deactivationRange)
                {

                    // Detener la animación de caminar si está fuera del rango de detección
                    animator.ResetTrigger("Atacar");

                    isActivated = false;
                    animator.SetTrigger("Idle");
                    animator.SetBool("isRunning", false);
                }

                // Si el enemigo ha sido activado, sigue la lógica de movimiento y ataque
                if (isActivated)
                {
                    if (distanceToPlayer <= attackRange)
                    {
                        // En rango de ataque
                        animator.SetTrigger("Atacar");
                        animator.SetBool("isRunning", false);

                        // Controlar el tiempo entre ataques
                        if (Time.time >= tiempoDesdeUltimoAtaque + tiempoEntreAtaques)
                        {
                            vidaJugador.RecibirDaño(daño); // Aplica el daño al jugador
                            tiempoDesdeUltimoAtaque = Time.time;
                        }
                    }
                    else
                    {
                        // Fuera del rango de ataque, pero dentro del rango de seguimiento
                        animator.SetBool("isRunning", true);

                        // Movimiento y rotación hacia el jugador
                        Vector3 direction = (player.position - transform.position).normalized;
                        transform.position += direction * speed * Time.deltaTime;

                        Quaternion targetRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    }

                }
            }
        }
    }