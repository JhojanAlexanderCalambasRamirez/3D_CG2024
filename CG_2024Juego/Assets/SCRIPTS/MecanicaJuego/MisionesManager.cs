using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MisionesManager : MonoBehaviour
{
    public GameObject panelMisiones;           // Panel de misiones
    public Button botonAbrirPanel;             // Botón para abrir el panel de misiones
    public Button botonCerrarPanel;            // Botón para cerrar el panel de misiones
    public Button[] botonesMisiones;           // Botones para iniciar las misiones
    public Slider[] slidersMisiones;           // Sliders para mostrar el progreso de las misiones
    public TextMeshProUGUI[] textosMisiones;   // Textos para mostrar el contexto de las misiones

    private bool[] misionesCompletadas = new bool[4]; // Estado de cada misión
    private int misionActiva = -1;                    // Índice de la misión activa (-1 si no hay ninguna activa)

    void Start()
    {
        // Asegurarse de que el panel esté cerrado al inicio
        panelMisiones.SetActive(false);

        // Agregar listeners a los botones
        botonAbrirPanel.onClick.AddListener(AbrirPanel);
        botonCerrarPanel.onClick.AddListener(CerrarPanel);

        // Configurar los botones de misión
        for (int i = 0; i < botonesMisiones.Length; i++)
        {
            int index = i;  // Captura del índice
            botonesMisiones[i].onClick.AddListener(() => IniciarMision(index));

            // Inicializar el estado visual de los sliders
            slidersMisiones[i].gameObject.SetActive(true);  // Mostrar sliders al inicio
            slidersMisiones[i].value = misionesCompletadas[i] ? 1 : 0;  // Mostrar progreso de misiones completadas
            slidersMisiones[i].fillRect.GetComponent<Image>().color = misionesCompletadas[i] ? Color.green : Color.red;  // Color verde si está completa
        }
    }

    void AbrirPanel()
    {
        panelMisiones.SetActive(true);   // Abre el panel de misiones
    }

    void CerrarPanel()
    {
        panelMisiones.SetActive(false);  // Cierra el panel de misiones
    }

    void IniciarMision(int index)
    {
        if (misionesCompletadas[index] || misionActiva != -1) return;  // Evitar reiniciar misión completada o iniciar otra mientras una esté activa

        misionActiva = index;
        slidersMisiones[index].value = 0;  // Resetear el progreso de la misión
        slidersMisiones[index].fillRect.GetComponent<Image>().color = Color.red;  // Cambiar el color a rojo para misión en progreso

        // Desactivar todos los botones de misión excepto el activo
        for (int i = 0; i < botonesMisiones.Length; i++)
        {
            botonesMisiones[i].interactable = (i == index); // Solo el botón de la misión activa es interactivo
        }
    }

    public void CompletarMision(string nombreEspada)
    {
        if (misionActiva == -1) return;  // Verificar que haya una misión activa

        bool misionCompletada = false;

        // Comprobación de nombre de espada para completar la misión actual
        switch (misionActiva)
        {
            case 0:
                if (nombreEspada == "Sword_Basica") misionCompletada = true;
                break;
            case 1:
                if (nombreEspada == "Sword_Red") misionCompletada = true;
                break;
            case 2:
                if (nombreEspada == "Sword_Green") misionCompletada = true;
                break;
            case 3:
                if (nombreEspada == "Sword_Blue") misionCompletada = true;
                break;
        }

        if (misionCompletada)
        {
            // Marcar misión como completada visualmente y en el estado
            slidersMisiones[misionActiva].value = 1;  // Llenar el slider al 100%
            slidersMisiones[misionActiva].fillRect.GetComponent<Image>().color = Color.green;  // Cambiar el color a verde
            misionesCompletadas[misionActiva] = true;
            misionActiva = -1;  // No hay misión activa actualmente

            // Reactivar botones para las misiones incompletas
            for (int i = 0; i < botonesMisiones.Length; i++)
            {
                if (!misionesCompletadas[i])  // Solo misiones incompletas son reactivables
                    botonesMisiones[i].interactable = true;
            }
        }
    }
}
