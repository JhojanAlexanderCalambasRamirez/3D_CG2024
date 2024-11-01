using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 2.0f;           // Velocidad de movimiento del enemigo
    public float rotationSpeed = 5.0f;   // Velocidad de rotación para seguir al jugador
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
        if (player != null && vidaJugador != null)
        {
            // Calcula la distancia entre el enemigo y el jugador
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

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
                // Movimiento y rotación hacia el jugador si está fuera del rango de ataque
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

                // Rotación hacia el jugador
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
