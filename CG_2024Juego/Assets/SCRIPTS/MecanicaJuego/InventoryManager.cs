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

        // Activar arma solo si el slot es válido (3 a 6) y el jugador la ha recogido
        if (index >= 2 && index <= 5) // Cambiar el rango de 3-6 a 2-5 para que funcione con SelectSlot(2) para Sword_Basica
        {
            int armaIndex = index - 2; // Cambiar a index - 2 para que corresponda correctamente
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

    // Funciones para recoger cada espada y asignarlas al slot correspondiente
    public void CollectSwordBasica()
    {
        armasRecogidas[0] = true;
        SelectSlot(2); // Asignar Sword_Basica al slot 3 y cambiar a él
        UpdateSlotUI(); // Asegura que se actualice la UI al momento de recoger
    }

    public void CollectSwordRed()
    {
        armasRecogidas[1] = true;
        SelectSlot(3); // Asignar Sword_Red al slot 4 y cambiar a él
        UpdateSlotUI();
    }

    public void CollectSwordGreen()
    {
        armasRecogidas[2] = true;
        SelectSlot(4); // Asignar Sword_Green al slot 5 y cambiar a él
        UpdateSlotUI();
    }

    public void CollectSwordBlue()
    {
        armasRecogidas[3] = true;
        SelectSlot(5); // Asignar Sword_Blue al slot 6 y cambiar a él
        UpdateSlotUI();
    }
}
