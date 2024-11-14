using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int dañoPuño = 20;
    public int dañoSword_Basica = 30;
    public int dañoSword_Red = 35;
    public int dañoSword_Green = 40;
    public int dañoSword_Blue = 45;
    public float tiempoEntreAtaques = 0.5f;
    public string tagEnemigo = "Enemigo"; // Tag para enemigos regulares

    private float tiempoProximoAtaque;
    private bool hasSword = false;
    private int selectedWeaponIndex = -1;

    private CogerArmas cogerArmas;

    private void Start()
    {
        cogerArmas = GetComponent<CogerArmas>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= tiempoProximoAtaque)
        {
            tiempoProximoAtaque = Time.time + tiempoEntreAtaques;
            Atacar();
        }
    }

    private void Atacar()
    {
        int daño = hasSword ? ObtenerDañoArma(selectedWeaponIndex) : dañoPuño;
        Debug.Log("Ataque ejecutado, daño aplicado: " + daño);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f))
        {
            // Verifica si el ataque golpea a un enemigo o al "Jefe"
            if (hit.transform.CompareTag(tagEnemigo) || hit.transform.CompareTag("Jefe"))
            {
                // Accede al script de vida del enemigo o jefe para aplicar el daño
                VidaEnemigo enemigo = hit.transform.GetComponent<VidaEnemigo>();
                if (enemigo != null)
                {
                    enemigo.RecibirDaño(daño);
                }
            }
        }
    }

    public void OnInventorySlotChanged(int slotIndex)
    {
        if (slotIndex >= 2 && slotIndex < 6)
        {
            hasSword = true;
            selectedWeaponIndex = slotIndex - 2;
            cogerArmas.ActivarArmar(selectedWeaponIndex);
            Debug.Log("Arma seleccionada: " + selectedWeaponIndex);
        }
        else
        {
            hasSword = false;
            selectedWeaponIndex = -1;
            cogerArmas.DesactivarArmas();
            Debug.Log("Sin arma seleccionada");
        }
    }

    private int ObtenerDañoArma(int index)
    {
        int daño = 0;
        switch (index)
        {
            case 0: daño = dañoSword_Basica; break;
            case 1: daño = dañoSword_Red; break;
            case 2: daño = dañoSword_Green; break;
            case 3: daño = dañoSword_Blue; break;
            default: daño = dañoPuño; break;
        }
        Debug.Log("Daño calculado para el arma " + index + ": " + daño);
        return daño;
    }
}
