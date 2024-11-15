using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuntoGuardado : MonoBehaviour
{
    private PlayerScoreManager scoreManager;
    private InventoryManager inventoryManager;

    private void Start()
    {
        // Referencias a otros scripts
        scoreManager = FindObjectOfType<PlayerScoreManager>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            GuardarProgreso();
            Debug.Log("Juego guardado en el punto de guardado.");
        }
    }

    [System.Serializable]
    public class ArmasRecogidasData
    {
        public List<bool> armasRecogidas;
    }

    private void GuardarProgreso()
    {
        // Guardar datos de PlayerScoreManager
        PlayerPrefs.SetInt("contadorEnemigos", scoreManager.contadorEnemigos);
        PlayerPrefs.SetInt("puntosAcumulados", scoreManager.puntosAcumulados);
        PlayerPrefs.SetInt("jefesMatados", scoreManager.jefesMatados);
        PlayerPrefs.SetInt("itemsColeccionados", scoreManager.itemsColeccionados);
        PlayerPrefs.SetInt("espadasElementales", scoreManager.espadasElementales);

        // Guardar datos de InventoryManager usando JSON
        ArmasRecogidasData armasRecogidasData = new ArmasRecogidasData
        {
            armasRecogidas = new List<bool>(inventoryManager.armasRecogidas)
        };
        string armasRecogidasJson = JsonUtility.ToJson(armasRecogidasData);
        PlayerPrefs.SetString("armasRecogidas", armasRecogidasJson);

        // Guardar el slot seleccionado
        PlayerPrefs.SetInt("selectedSlot", inventoryManager.selectedSlot);

        PlayerPrefs.Save();
    }

    public void CargarProgreso()
    {
        // Cargar datos de PlayerScoreManager
        scoreManager.contadorEnemigos = PlayerPrefs.GetInt("contadorEnemigos", 0);
        scoreManager.puntosAcumulados = PlayerPrefs.GetInt("puntosAcumulados", 0);
        scoreManager.jefesMatados = PlayerPrefs.GetInt("jefesMatados", 0);
        scoreManager.itemsColeccionados = PlayerPrefs.GetInt("itemsColeccionados", 0);
        scoreManager.espadasElementales = PlayerPrefs.GetInt("espadasElementales", 0);

        scoreManager.ActualizarUI();

        // Cargar datos de InventoryManager desde JSON
        if (PlayerPrefs.HasKey("armasRecogidas"))
        {
            string armasRecogidasJson = PlayerPrefs.GetString("armasRecogidas");
            ArmasRecogidasData armasRecogidasData = JsonUtility.FromJson<ArmasRecogidasData>(armasRecogidasJson);
            inventoryManager.armasRecogidas = armasRecogidasData.armasRecogidas.ToArray();
        }

        // Cargar el slot seleccionado
        inventoryManager.SelectSlot(PlayerPrefs.GetInt("selectedSlot", 0));
    }
}
