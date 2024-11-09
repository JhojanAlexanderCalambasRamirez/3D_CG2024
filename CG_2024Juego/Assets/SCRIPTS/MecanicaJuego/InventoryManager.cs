using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Button[] slots; // Array de botones que representa los 6 slots del inventario
    public Image[] slotImages; // Array de imágenes en los slots
    public GameObject[] objectsInInventory; // Objetos en el inventario
    public PlayerMove playerMove; // Referencia al PlayerMove

    private int selectedSlot = -1; // Slot seleccionado (-1 significa que ninguno está seleccionado)
    private float inactiveOpacity = 0.5f; // Opacidad para los slots no seleccionados

    void Start()
    {
        DeselectAllSlots();
        UpdateSlotImages();
    }

    void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SelectSlot(i);
                break;
            }
        }
    }

    void SelectSlot(int index)
    {
        selectedSlot = index;
        UpdateSlotUI();
        Debug.Log("Slot seleccionado: " + (index + 1));
        ActivateObjectInSlot(index);

        // Notificar al PlayerMove sobre el cambio de slot
        playerMove.OnInventorySlotChanged(selectedSlot);
    }

    void ActivateObjectInSlot(int slotIndex)
    {
        for (int i = 0; i < objectsInInventory.Length; i++)
        {
            if (objectsInInventory[i] != null)
                objectsInInventory[i].SetActive(false);
        }

        if (slotIndex >= 0 && slotIndex < objectsInInventory.Length)
        {
            GameObject selectedObject = objectsInInventory[slotIndex];

            if (selectedObject != null)
            {
                selectedObject.SetActive(true);
                Debug.Log("Objeto activado: " + selectedObject.name);
            }
        }

        UpdateSlotImages();
    }

    void UpdateSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            ColorBlock colors = slots[i].colors;

            if (i == selectedSlot)
            {
                colors.normalColor = Color.yellow; // Cambia el color del slot seleccionado
            }
            else
            {
                colors.normalColor = Color.white; // Restaura el color de los slots no seleccionados
            }

            slots[i].colors = colors;
        }
    }

    void UpdateSlotImages()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (objectsInInventory[i] != null)
            {
                Color color = slotImages[i].color;
                color.a = (i == selectedSlot) ? 1f : inactiveOpacity;
                slotImages[i].color = color;
                slotImages[i].enabled = true;
            }
            else
            {
                slotImages[i].enabled = false;
            }
        }
    }

    void DeselectAllSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            ColorBlock colors = slots[i].colors;
            colors.normalColor = Color.white;
            slots[i].colors = colors;
        }
    }
}
