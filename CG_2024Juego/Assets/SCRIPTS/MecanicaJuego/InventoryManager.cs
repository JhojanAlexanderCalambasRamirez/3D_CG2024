using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Button[] slots; // Array de botones que representa los 6 slots del inventario
    private int selectedSlot = -1; // Slot seleccionado (-1 significa que ninguno está seleccionado)
    public CogerArmas cogerArmas; // Referencia al script que maneja las armas
    public PlayerMove playerMove; // Referencia al script de movimiento del jugador


    // Asignación manual de espadas
    private void Start()
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
            playerMove.OnInventorySlotChanged(0); // Actualizar animación en PlayerMove
        }
        else if (index == 2)
        {
            // Slot 3: Sword_Basica
            cogerArmas.ActivarArmar(0); // Activa Sword_Basica
            playerMove.OnInventorySlotChanged(3);
        }
        else if (index == 3)
        {
            // Slot 4: Sword_Red
            cogerArmas.ActivarArmar(1); // Activa Sword_Red
            playerMove.OnInventorySlotChanged(4);
        }
        else if (index == 4)
        {
            // Slot 5: Sword_Green
            cogerArmas.ActivarArmar(2); // Activa Sword_Green
            playerMove.OnInventorySlotChanged(5);
        }
        else if (index == 5)
        {
            // Slot 6: Sword_Blue
            cogerArmas.ActivarArmar(3); // Activa Sword_Blue
            playerMove.OnInventorySlotChanged(6);
        }
        else
        {
            // Otros slots que no son de armas
            cogerArmas.DesactivarArmas();
            playerMove.OnInventorySlotChanged(index); // Actualizar animación en PlayerMove
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
