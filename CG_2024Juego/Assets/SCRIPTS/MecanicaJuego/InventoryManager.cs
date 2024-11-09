using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Button[] slots;
    private int selectedSlot = -1;
    public CogerArmas cogerArmas;
    public PlayerMove playerMove;

    public GameObject swordBasica;
    public GameObject swordRed;
    public GameObject swordGreen;
    public GameObject swordBlue;

    private bool hasSwordBasica = false;
    private bool hasSwordRed = false;
    private bool hasSwordGreen = false;
    private bool hasSwordBlue = false;

    void Start()
    {
        SelectSlot(0);
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

        if (index == 0)
        {
            cogerArmas.DesactivarArmas();
            playerMove.OnInventorySlotChanged(0);
        }
        else if (index == 3 && hasSwordBasica)
        {
            cogerArmas.ActivarArmar(0);
            playerMove.OnInventorySlotChanged(index);
        }
        else if (index == 4 && hasSwordRed)
        {
            cogerArmas.ActivarArmar(1);
            playerMove.OnInventorySlotChanged(index);
        }
        else if (index == 5 && hasSwordGreen)
        {
            cogerArmas.ActivarArmar(2);
            playerMove.OnInventorySlotChanged(index);
        }
        else if (index == 6 && hasSwordBlue)
        {
            cogerArmas.ActivarArmar(3);
            playerMove.OnInventorySlotChanged(index);
        }
        else
        {
            cogerArmas.DesactivarArmas();
            playerMove.OnInventorySlotChanged(0);
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
        hasSwordBasica = true;
    }

    public void CollectSwordRed()
    {
        hasSwordRed = true;
    }

    public void CollectSwordGreen()
    {
        hasSwordGreen = true;
    }

    public void CollectSwordBlue()
    {
        hasSwordBlue = true;
    }
}
