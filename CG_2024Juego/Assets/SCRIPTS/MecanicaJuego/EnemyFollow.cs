using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 2.0f;           // Velocidad de movimiento del enemigo
    public float rotationSpeed = 5.0f;   // Velocidad de rotación para seguir al jugador
    public float detectionRange = 10.0f; // Distancia a la cual el enemigo empieza a seguir al jugador
    public float attackRange = 2.0f;     // Distancia a la que el enemigo atacará al jugador
    public float daño = 10f;             // Cantidad de daño que el enemigo hace al jugador
    public float tiempoEntreAtaques = 1.0f; // Tiempo en segundos entre ataques

    private Transform player;
    private Vida vidaJugador;
    private Animator animator;
    private float tiempoDesdeUltimoAtaque;

    void Start()
    {
        // Encuentra al jugador por su tag y obtiene el componente Vida
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player1");
        if (playerObj != null)
        {
            player = playerObj.transform;
            vidaJugador = playerObj.GetComponent<Vida>(); // Obtiene el componente Vida del jugador

            // Verifica si el componente Vida está presente
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
    }

    void Update()
    {
        if (player != null && vidaJugador != null && animator != null)
        {
            // Calcula la distancia entre el enemigo y el jugador
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Si el jugador está dentro del rango de detección
            if (distanceToPlayer <= detectionRange)
            {
                // Si está dentro del rango de ataque
                if (distanceToPlayer <= attackRange)
                {
                    // Activar la animación de ataque
                    animator.SetTrigger("Atacar");

                    // Controlar el tiempo entre ataques
                    if (Time.time >= tiempoDesdeUltimoAtaque + tiempoEntreAtaques)
                    {
                        vidaJugador.RecibirDaño(daño); // Llama a RecibirDaño en el script Vida del jugador
                        tiempoDesdeUltimoAtaque = Time.time; // Actualiza el tiempo del último ataque
                    }
                }
                else
                {
                    // Movimiento y rotación hacia el jugador si está dentro del rango de detección pero fuera del rango de ataque
                    Vector3 direction = (player.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;

                    // Rotación hacia el jugador
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                // Detener la animación de caminar si está fuera del rango de detección
                animator.ResetTrigger("Atacar");
            }
        }
    }
}
