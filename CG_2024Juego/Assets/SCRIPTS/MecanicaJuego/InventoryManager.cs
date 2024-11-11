using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Button[] slots; // Array de botones de slots de inventario
    private int selectedSlot = -1;
    public CogerArmas cogerArmas;
    public PlayerMove playerMove;

    // Flags para determinar si cada espada ha sido recogida
    private bool[] armasRecogidas = new bool[4]; // [Sword_Basica, Sword_Red, Sword_Green, Sword_Blue]

    void Start()
    {
        SelectSlot(0); // Seleccionar el primer slot (sin arma) al inicio
        Debug.Log("Cantidad de slots: " + slots.Length);

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                Debug.LogWarning("Slot " + (i + 1) + " es nulo.");
            }
            else
            {
                Debug.Log("Slot " + (i + 1) + " tiene tag: " + slots[i].tag);
                // Validación de tags en cada slot
                switch (i)
                {
                    case 2: // Slot 3 en index 2
                        if (slots[i].tag != "SaveSword_Basica")
                            Debug.LogWarning("El tag del slot 3 no coincide con SaveSword_Basica");
                        break;
                    case 3: // Slot 4 en index 3
                        if (slots[i].tag != "SaveSword_Red")
                            Debug.LogWarning("El tag del slot 4 no coincide con SaveSword_Red");
                        break;
                    case 4: // Slot 5 en index 4
                        if (slots[i].tag != "SaveSword_Green")
                            Debug.LogWarning("El tag del slot 5 no coincide con SaveSword_Green");
                        break;
                    case 5: // Slot 6 en index 5
                        if (slots[i].tag != "SaveSword_Blue")
                            Debug.LogWarning("El tag del slot 6 no coincide con SaveSword_Blue");
                        break;
                }
            }
        }
    }

    void Update()
    {
        // Detectar teclas 1, 2, 3, etc. para seleccionar el slot correspondiente
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

        // Desactivar todas las armas antes de activar la seleccionada
        cogerArmas.DesactivarArmas();
        playerMove.OnInventorySlotChanged(0);

        // Activar arma solo si el slot es uno de los slots asignados para espadas y el jugador la ha recogido
        if (index > 2 && index < 6) // Validación para slots 3 a 6
        {
            int armaIndex = index - 2; // Correspondencia del índice del slot con el índice de armas
            if (armasRecogidas[armaIndex]) // Verifica si el arma fue recogida
            {
                cogerArmas.ActivarArmar(armaIndex);
                playerMove.OnInventorySlotChanged(index);
            }
        }
    }

    void UpdateSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            ColorBlock colors = slots[i].colors;

            if (i == selectedSlot)
            {
                colors.normalColor = Color.yellow;
                slots[i].image.color = Color.white;
            }
            else
            {
                colors.normalColor = Color.white;
                slots[i].image.color = new Color(1f, 1f, 1f, 0.3f);
            }

            slots[i].colors = colors;
        }
    }

    // Funciones para recoger cada espada y asignarlas al slot correspondiente solo si el tag coincide
    public void CollectSwordBasica()
    {
        AssignSwordToSlot("SaveSword_Basica", 2, 0); // Tag, SlotIndex, ArmaIndex
    }

    public void CollectSwordRed()
    {
        AssignSwordToSlot("SaveSword_Red", 3, 1);
    }

    public void CollectSwordGreen()
    {
        AssignSwordToSlot("SaveSword_Green", 4, 2);
    }

    public void CollectSwordBlue()
    {
        AssignSwordToSlot("SaveSword_Blue", 5, 3);
    }

    // Método para asignar la espada al slot correcto si el tag coincide
    void AssignSwordToSlot(string requiredTag, int slotIndex, int armaIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length)
        {
            Debug.LogWarning("El índice " + slotIndex + " está fuera de los límites del array.");
            return;
        }

        if (slots[slotIndex].tag != requiredTag)
        {
            Debug.LogWarning("El tag del slot " + (slotIndex + 1) + " no coincide con " + requiredTag);
        }
        else
        {
            Debug.Log("Asignando " + requiredTag + " al slot " + (slotIndex + 1));
            armasRecogidas[armaIndex] = true; // Marca el arma como recogida

            // Selecciona el slot automáticamente al recoger la espada y muestra la animación
            SelectSlot(slotIndex);
            cogerArmas.ActivarArmar(armaIndex);
            playerMove.OnInventorySlotChanged(slotIndex);
        }
    }
}
