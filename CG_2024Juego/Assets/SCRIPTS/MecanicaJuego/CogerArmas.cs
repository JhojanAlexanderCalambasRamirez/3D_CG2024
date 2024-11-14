using UnityEngine;

/**
* Clase que maneja la recolección y activación de armas en el juego.
* Permite activar o desactivar armas, obtener el daño de las armas y el puño, y registrar la recolección de nuevas armas en el inventario.
* @author Alexander Calambas - 2190555
* @author Juan Manuel Santos - 2215928
* @author Juan David Rios - 2225674
* @date 11 noviembre 2024
* @version 1.0
*/
public class CogerArmas : MonoBehaviour
{
    public GameObject[] armas; // Array de espadas en la escena (ordenadas en el mismo orden que en InventoryManager)
    public int[] dañoArmas = { 30, 35, 40, 45 }; // Daño específico para Sword_Basica, Sword_Red, Sword_Green, y Sword_Blue en este orden

    /**
   * Desactiva todas las armas en la escena.
   * Se utiliza para asegurarse de que no haya armas activas en la escena al inicio o al desecharlas.
   * @return Ninguno
   */
    public void DesactivarArmas()
    {
        foreach (GameObject arma in armas)
        {
            arma.SetActive(false);
        }
    }
    /**
 * Devuelve el daño del puño del personaje.
 * Este valor es constante y representa el daño que causa el puño.
 * @return El valor de daño del puño (10)
 */

    public int ObtenerDañoPuño()
    {
        return 10; // Valor del daño del puño
    }

    /**
   * Activa un arma específica en función de su índice en el array de armas.
   * El índice debe estar dentro del rango de armas disponibles.
   * @param index El índice del arma a activar (debe estar entre 0 y armas.Length - 1)
   * @return Ninguno
   */
    public void ActivarArmar(int index)
    {
        if (index >= 0 && index < armas.Length)
        {
            armas[index].SetActive(true);
        }
    }

    /**
    * Devuelve el daño asociado con un arma específica según su índice.
    * Si el índice no es válido, devuelve 0.
    * @param index El índice del arma (debe estar entre 0 y dañoArmas.Length - 1)
    * @return El daño del arma correspondiente al índice, o 0 si el índice no es válido
    */
    public int ObtenerDañoArma(int index)
    {
        if (index >= 0 && index < dañoArmas.Length)
        {
            return dañoArmas[index];
        }
        return 0;
    }
    /**
   * Recoge un arma específica, activándola y registrándola en el inventario.
   * Asigna cada índice a la función de recolección correspondiente.
   * @param index El índice del arma a recoger (debe ser entre 0 y 3)
   * @return Ninguno
   */
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
