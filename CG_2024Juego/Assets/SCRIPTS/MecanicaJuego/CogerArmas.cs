using UnityEngine;

public class CogerArmas : MonoBehaviour
{
    public GameObject[] armas; // Array de espadas en la escena (ordenadas en el mismo orden que en InventoryManager)
    public int[] dañoArmas = { 30, 35, 40, 45 }; // Daño específico para Sword_Basica, Sword_Red, Sword_Green, y Sword_Blue en este orden

    public void DesactivarArmas()
    {
        foreach (GameObject arma in armas)
        {
            arma.SetActive(false);
        }
    }
    public int ObtenerDañoPuño()
    {
        return 10; // Valor del daño del puño
    }

    public void ActivarArmar(int index)
    {
        if (index >= 0 && index < armas.Length)
        {
            armas[index].SetActive(true);
        }
    }

    public int ObtenerDañoArma(int index)
    {
        if (index >= 0 && index < dañoArmas.Length)
        {
            return dañoArmas[index];
        }
        return 0;
    }

    public void RecogerArma(int index)
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();

        // Asignamos cada índice a la función de recolección adecuada sin ajustes adicionales
        switch (index)
        {
            case 0: inventoryManager.CollectSwordBasica(); break;
            case 1: inventoryManager.CollectSwordRed(); break;
            case 2: inventoryManager.CollectSwordGreen(); break;
            case 3: inventoryManager.CollectSwordBlue(); break;
        }
    }
}
