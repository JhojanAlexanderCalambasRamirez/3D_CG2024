using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 2.0f;           // Velocidad de movimiento del enemigo
    public float rotationSpeed = 5.0f;   // Velocidad de rotación para seguir al jugador
    public float attackRange = 2.0f;     // Distancia a la que el enemigo atacará al jugador

    private Transform player;
    private Animator animator;

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

            // Si está dentro del rango de ataque, realiza la animación de ataque
            if (distanceToPlayer <= attackRange)
            {
                // Activar la animación de ataque
                animator.SetTrigger("Atacar");
            }
            else
            {
                // Movimiento y rotación hacia el jugador
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

                // Rotación hacia el jugador
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
