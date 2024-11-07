using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Brujo : MonoBehaviour
{
    public float speed = 2.0f;           // Velocidad de movimiento del enemigo
    public float rotationSpeed = 5.0f;   // Velocidad de rotación para seguir al jugador
    public float attackRange = 2.0f;     // Distancia a la que el enemigo atacará al jugador
    public GameObject bolaDeFuegoPrefab; // Prefab de la bola de fuego
    public Transform spawnPoint;         // Punto de salida de la bola de fuego
    public float fuerzaDeDisparo = 10f;  // Fuerza con la que se lanza la bola de fuego

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

    // Método que será llamado desde el evento de animación
    public void DispararBolaDeFuego()
    {
        if (player == null) return;

        // Instancia la bola de fuego en el punto de salida
        GameObject bolaDeFuego = Instantiate(bolaDeFuegoPrefab, spawnPoint.position, Quaternion.identity);

        // Calcula la dirección hacia el jugador
        Vector3 direccion = (player.position - spawnPoint.position).normalized;

        // Aplica la fuerza para lanzar la bola de fuego
        bolaDeFuego.GetComponent<Rigidbody>().AddForce(direccion * fuerzaDeDisparo, ForceMode.Impulse);
    }
}
