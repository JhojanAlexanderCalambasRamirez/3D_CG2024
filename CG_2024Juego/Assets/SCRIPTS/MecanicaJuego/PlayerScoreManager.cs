using UnityEngine;
using TMPro;

public class PlayerScoreManager : MonoBehaviour
{
    public TextMeshProUGUI contadorEnemigosText;
    public TextMeshProUGUI puntosAcumuladosEnemigoText;
    public TextMeshProUGUI jefesMatadosText;
    public TextMeshProUGUI itemsColeccionadosText;

    private int contadorEnemigos = 0;
    private int puntosAcumulados = 0;
    private int jefesMatados = 0;
    private int itemsColeccionados = 0;

    // Método para actualizar la interfaz de usuario
    private void ActualizarUI()
    {
        contadorEnemigosText.text = contadorEnemigos.ToString();
        puntosAcumuladosEnemigoText.text = puntosAcumulados.ToString();
        jefesMatadosText.text = jefesMatados.ToString();
        itemsColeccionadosText.text = itemsColeccionados.ToString();
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
}
