using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class FinalSceneManager : MonoBehaviour
{
    public TextMeshProUGUI nombreJugadorText;
    public TextMeshProUGUI contadorEnemigosText;
    public TextMeshProUGUI puntosAcumuladosEnemigoText;
    public TextMeshProUGUI jefesMatadosText;
    public TextMeshProUGUI itemsColeccionadosText;
    public TextMeshProUGUI espadasElementalesText;
    public TextMeshProUGUI muertesJugadorText;

    private void Start()
    {
        // Cargar y mostrar los datos almacenados en PlayerPrefs
        nombreJugadorText.text = PlayerPrefs.GetString("nombre1", "Jugador");
        contadorEnemigosText.text = PlayerPrefs.GetInt("contadorEnemigos", 0).ToString();
        puntosAcumuladosEnemigoText.text = PlayerPrefs.GetInt("puntosAcumulados", 0).ToString();
        jefesMatadosText.text = PlayerPrefs.GetInt("jefesMatados", 0).ToString();
        itemsColeccionadosText.text = PlayerPrefs.GetInt("itemsColeccionados", 0).ToString();
        espadasElementalesText.text = PlayerPrefs.GetInt("espadasElementales", 0).ToString();
        muertesJugadorText.text = PlayerPrefs.GetInt("muertesJugador", 0).ToString();
    }

    [System.Serializable]
    public class DatosJugador
    {
        public string nombreJugador;
        public int contadorEnemigos;
        public int puntosAcumulados;
        public int jefesMatados;
        public int itemsColeccionados;
        public int espadasElementales;
        public int muertesJugador;
    }

    public void GuardarDatosComoJson()
    {
        DatosJugador datosJugador = new DatosJugador
        {
            nombreJugador = PlayerPrefs.GetString("nombre1", "Jugador"),
            contadorEnemigos = PlayerPrefs.GetInt("contadorEnemigos", 0),
            puntosAcumulados = PlayerPrefs.GetInt("puntosAcumulados", 0),
            jefesMatados = PlayerPrefs.GetInt("jefesMatados", 0),
            itemsColeccionados = PlayerPrefs.GetInt("itemsColeccionados", 0),
            espadasElementales = PlayerPrefs.GetInt("espadasElementales", 0),
            muertesJugador = PlayerPrefs.GetInt("muertesJugador", 0)
        };

        string json = JsonUtility.ToJson(datosJugador, true);
        string path = Path.Combine(Application.persistentDataPath, "datosJugador.json");
        File.WriteAllText(path, json);

        Debug.Log("Datos guardados en JSON en: " + path);
    }

    // Método para el botón "Volver al Menú Principal"
    public void VolverAlMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
