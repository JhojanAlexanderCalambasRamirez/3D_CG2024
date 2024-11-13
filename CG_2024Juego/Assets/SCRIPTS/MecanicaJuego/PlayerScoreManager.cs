using UnityEngine;
using TMPro;

public class PlayerScoreManager : MonoBehaviour
{
    public TextMeshProUGUI contadorEnemigosText;
    public TextMeshProUGUI puntosAcumuladosEnemigoText;
    public TextMeshProUGUI jefesMatadosText;
    public TextMeshProUGUI itemsColeccionadosText;
    public TextMeshProUGUI espadasElementalesText; // Nuevo TextMeshProUGUI para espadas elementales

    public int contadorEnemigos = 0;
    public int puntosAcumulados = 0;
    public int jefesMatados = 0;
    public int itemsColeccionados = 0;
    public int espadasElementales = 0; // Nuevo contador para las espadas elementales

    // Método para actualizar la interfaz de usuario
    public void ActualizarUI()
    {
        contadorEnemigosText.text = contadorEnemigos.ToString();
        puntosAcumuladosEnemigoText.text = puntosAcumulados.ToString();
        jefesMatadosText.text = jefesMatados.ToString();
        itemsColeccionadosText.text = itemsColeccionados.ToString();
        espadasElementalesText.text = espadasElementales.ToString(); // Actualiza el contador de espadas elementales
    }

    // Método para llamar cuando un enemigo normal es derrotado
    public void EnemigoDerrotado()
    {
        contadorEnemigos++;
        puntosAcumulados += 10;
        ActualizarUI();
    }

    // Método para llamar cuando un jefe es derrotado
    public void JefeDerrotado()
    {
        jefesMatados++;
        puntosAcumulados += 50; // O cualquier valor que desees para jefes
        ActualizarUI();
    }

    // Método para actualizar el contador de ítems recogidos
    public void ItemRecogido()
    {
        itemsColeccionados++;
        ActualizarUI();
    }

    // Nuevo método para llamar cuando se recoge una espada elemental
    public void EspadaRecogida()
    {
        espadasElementales++; // Incrementa el contador de espadas elementales
        ActualizarUI();
    }

    // Método para detectar colisiones con objetos
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            EspadaRecogida(); // Llama al método cuando colisiona con un objeto con el tag "Sword"
            Destroy(other.gameObject); // Destruye la espada en la escena después de recogerla
        }
        else if (other.CompareTag("Items"))
        {
            ItemRecogido(); // Llama al método cuando colisiona con un objeto con el tag "Items"
            Destroy(other.gameObject); // Destruye el ítem en la escena después de recogerlo
        }
    }
}
