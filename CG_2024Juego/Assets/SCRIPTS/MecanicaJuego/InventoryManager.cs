using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public Button[] slots;
    public TextMeshProUGUI nombreObjetoSlot;  // Referencia al TextMeshPro para mostrar el nombre del objeto
    public int selectedSlot = -1;
    public CogerArmas cogerArmas;
    public PlayerMove playerMove;
    public MisionesManager misionesManager;

    public bool[] armasRecogidas = new bool[4];

    void Start()
    {
        SelectSlot(0);

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                Debug.LogWarning("Slot " + (i + 1) + " es nulo.");
            }
            else
            {
                Debug.Log("Slot " + (i + 1) + " tiene tag: " + slots[i].tag);
                switch (i)
                {
                    case 2:
                        if (slots[i].tag != "SaveSword_Basica")
                            Debug.LogWarning("El tag del slot 3 no coincide con SaveSword_Basica");
                        break;
                    case 3:
                        if (slots[i].tag != "SaveSword_Red")
                            Debug.LogWarning("El tag del slot 4 no coincide con SaveSword_Red");
                        break;
                    case 4:
                        if (slots[i].tag != "SaveSword_Green")
                            Debug.LogWarning("El tag del slot 5 no coincide con SaveSword_Green");
                        break;
                    case 5:
                        if (slots[i].tag != "SaveSword_Blue")
                            Debug.LogWarning("El tag del slot 6 no coincide con SaveSword_Blue");
                        break;
                }
            }
        }
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

    public void SelectSlot(int index)
    {
        selectedSlot = index;
        UpdateSlotUI();

        cogerArmas.DesactivarArmas();
        playerMove.OnInventorySlotChanged(0);

        // Establecer el nombre del objeto en el TextMeshPro
        switch (index)
        {
            case 0:
            case 1:
                nombreObjetoSlot.text = "Puños";
                break;
            case 2:
                nombreObjetoSlot.text = "Espada Básica";
                break;
            case 3:
                nombreObjetoSlot.text = "Espada Roja";
                break;
            case 4:
                nombreObjetoSlot.text = "Espada Verde";
                break;
            case 5:
                nombreObjetoSlot.text = "Espada Azul";
                break;
            default:
                nombreObjetoSlot.text = "";  // Para slots no asignados
                break;
        }

        if (index > 1 && index < 6)
        {
            int armaIndex = index - 2;
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

    public void CollectSwordBasica()
    {
        AssignSwordToSlot("SaveSword_Basica", 2, 0);
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

        // Llama a CompletarMision en el MisionesManager para completar la misión
        misionesManager.CompletarMision("Sword_Blue");
    }

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
            armasRecogidas[armaIndex] = true;

            SelectSlot(slotIndex);
            cogerArmas.ActivarArmar(armaIndex);
            playerMove.OnInventorySlotChanged(slotIndex);
        }
    }
}
