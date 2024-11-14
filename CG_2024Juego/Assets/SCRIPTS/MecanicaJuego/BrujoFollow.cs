using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Clase que controla el comportamiento del enemigo Brujo en el juego.
* El Brujo persigue al jugador y lanza bolas de fuego cuando está en rango de ataque.
* @author Alexander Calambas - 2190555
* @author Juan Manuel Santos - 2215928
* @author Juan David Rios - 2225674
* @date 11 noviembre 2024
* @version 1.0
*/
public class Brujo : MonoBehaviour
{
    public float speed = 2.0f;           // Velocidad de movimiento del enemigo
    public float rotationSpeed = 5.0f;   // Velocidad de rotación para seguir al jugador
    public float attackRange = 2.0f;     // Distancia a la que el enemigo atacará al jugador
    public GameObject bolaDeFuegoPrefab; // Prefab de la bola de fuego
    public Transform spawnPoint;         // Punto de salida de la bola de fuego
    public float fuerzaDeDisparo = 10f;  // Fuerza con la que se lanza la bola de fuego
    public float activationRange = 10.0f; // Distancia para activar al enemigo

    private Transform player;
    private Animator animator;
    private bool isActivated = false;     // Indica si el enemigo ha sido activado


    /**
    * Método de inicialización que busca al jugador en la escena y configura el animador.
    * @param Ninguno
    * @return Ninguno
    */

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
        animator.SetTrigger("Idle"); // Comienza en animación Idle
    }

    /**
   * Actualiza la lógica del Brujo en cada frame, incluyendo su activación, movimiento y ataque.
   * @param Ninguno
   * @return Ninguno
   */

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Activa al enemigo si el jugador está dentro del rango de activación
            if (!isActivated && distanceToPlayer <= activationRange)
            {
                isActivated = true;
                animator.ResetTrigger("Idle"); // Quita Idle al activarse
            }

            // Lógica solo si el enemigo ha sido activado
            if (isActivated)
            {
                if (distanceToPlayer <= attackRange)
                {
                    // En rango de ataque
                    animator.SetTrigger("Atacar");
                    animator.SetBool("isWalking", false);
                }
                else
                {
                    // Fuera de rango de ataque, pero dentro del rango de seguimiento
                    animator.SetBool("isWalking", true);

                    // Movimiento y rotación hacia el jugador
                    Vector3 direction = (player.position - transform.position).normalized;
                    transform.position += direction * speed * Time.deltaTime;

                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                // Si no está activado, mantener Idle
                animator.SetTrigger("Idle");
                animator.SetBool("isWalking", false);
            }
        }
    }

    /**
    * Dispara una bola de fuego hacia la posición del jugador.
    * Este método se llama desde un evento de animación para crear y lanzar la bola de fuego.
    * @param Ninguno
    * @return Ninguno
    */
    public void DispararBolaDeFuego()
    {
        if (player == null) return;

        GameObject bolaDeFuego = Instantiate(bolaDeFuegoPrefab, spawnPoint.position, Quaternion.identity);
        Vector3 direccion = (player.position - spawnPoint.position).normalized;
        bolaDeFuego.GetComponent<Rigidbody>().AddForce(direccion * fuerzaDeDisparo, ForceMode.Impulse);
    }
}