using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Button[] slots;
    private int selectedSlot = -1;
    public CogerArmas cogerArmas;
    public PlayerMove playerMove;

    // Arreglo para verificar si cada espada ha sido recogida en su slot específico
    private bool[] armasRecogidas = new bool[4]; // [Sword_Basica, Sword_Red, Sword_Green, Sword_Blue]

    void Start()
    {
        SelectSlot(0); // Seleccionar el primer slot (sin espada) al inicio
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

        // Desactivar todas las armas primero
        cogerArmas.DesactivarArmas();
        playerMove.OnInventorySlotChanged(0);

        // Verificar y activar el arma en el slot específico solo si ha sido recogida
        if (index >= 3 && index <= 6)
        {
            // Ajustamos el índice para que coincida directamente con el slot y el arma recogida
            int armaIndex = index - 3;
            if (armasRecogidas[armaIndex])
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

    // Funciones para recoger cada espada y asignarla al slot correcto
    public void CollectSwordBasica()
    {
        armasRecogidas[0] = true; // Slot 3
        Debug.Log("Sword_Basica recogida y asignada al Slot 3");
    }

    public void CollectSwordRed()
    {
        armasRecogidas[1] = true; // Slot 4
        Debug.Log("Sword_Red recogida y asignada al Slot 4");
    }

    public void CollectSwordGreen()
    {
        armasRecogidas[2] = true; // Slot 5
        Debug.Log("Sword_Green recogida y asignada al Slot 5");
    }

    public void CollectSwordBlue()
    {
        armasRecogidas[3] = true; // Slot 6
        Debug.Log("Sword_Blue recogida y asignada al Slot 6");
    }
}
