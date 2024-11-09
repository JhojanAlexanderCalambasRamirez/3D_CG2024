using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Button[] slots; // Array de botones que representa los 6 slots del inventario
    private int selectedSlot = -1; // Slot seleccionado (-1 significa que ninguno está seleccionado)
    public CogerArmas cogerArmas;

    void Start()
    {
        SelectSlot(0); // Seleccionar automáticamente el primer slot al iniciar
    }

    void Update()
    {
        // Detección de teclas numéricas (1 a 6) para seleccionar el slot
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
                break;
            }
        }
    }

    // Función para seleccionar un slot
    void SelectSlot(int index)
    {
        selectedSlot = index;
        UpdateSlotUI();
        Debug.Log("Slot seleccionado: " + (index + 1)); // Muestra en la consola el slot seleccionado

        if (index == 0)
        {
            // Slot 1 seleccionado, desactivar todas las espadas
            cogerArmas.DesactivarArmas();
        }
        else if (index >= 3 && index <= 6)
        {
            // Activar espada específica en los slots 3 a 6
            cogerArmas.ActivarArmar(index - 3);
        }
        else
        {
            // Otros slots que no son de armas
            cogerArmas.DesactivarArmas();
        }
    }

    // Actualiza la UI para mostrar el slot seleccionado
    void UpdateSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            ColorBlock colors = slots[i].colors;

            if (i == selectedSlot)
            {
                colors.normalColor = Color.yellow; // Cambia el color del slot seleccionado
                slots[i].image.color = Color.white; // Imagen en el slot seleccionado en color normal
            }
            else
            {
                colors.normalColor = Color.white;
                slots[i].image.color = new Color(1f, 1f, 1f, 0.3f); // Los demás slots en opacidad reducida
            }

            slots[i].colors = colors;
        }
    }
}
