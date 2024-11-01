using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public float Salud = 100;
    public float SaludMaxima = 100;

    public Image BarraSalud;
    public Text TextoSalud;

    private SkinnedMeshRenderer[] meshRenderers;
    private List<Color[]> originalColors = new List<Color[]>();

    void Start()
    {
        // Obtener todos los SkinnedMeshRenderers del personaje
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        // Almacenar los colores originales de cada material
        foreach (var renderer in meshRenderers)
        {
            Color[] colors = new Color[renderer.materials.Length];
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                colors[i] = renderer.materials[i].color;
            }
            originalColors.Add(colors);
        }
    }

    void Update()
    {
        ActualizarInterfaz();
    }

    public void RecibirDaño(float daño)
    {
        Salud -= daño;
        ActualizarInterfaz();

        // Iniciar la corrutina para el efecto visual de daño
        StartCoroutine(MostrarDaño());
    }

    void ActualizarInterfaz()
    {
        BarraSalud.fillAmount = Salud / SaludMaxima;
        TextoSalud.text = "+ " + Salud.ToString("f0");
    }

    IEnumerator MostrarDaño()
    {
        // Rojo moderado en tono e intensidad
        Color moderateRed = new Color(0.6f, 0.2f, 0.2f, 1f); // Rojo menos saturado

        // Cambiar el color de todos los materiales a rojo moderado
        foreach (var renderer in meshRenderers)
        {
            foreach (var material in renderer.materials)
            {
                // Activar emisión con menor intensidad
                if (material.HasProperty("_EmissionColor"))
                {
                    material.EnableKeyword("_EMISSION");
                    material.SetColor("_EmissionColor", moderateRed * 0.6f); // Emisión moderada
                }

                // Cambiar el color principal al rojo moderado
                material.color = moderateRed;
            }
        }

        // Esperar un segundo
        yield return new WaitForSeconds(1f);

        // Restaurar el color original de cada material y desactivar emisión
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            for (int j = 0; j < meshRenderers[i].materials.Length; j++)
            {
                if (meshRenderers[i].materials[j].HasProperty("_EmissionColor"))
                {
                    meshRenderers[i].materials[j].DisableKeyword("_EMISSION");
                }
                meshRenderers[i].materials[j].color = originalColors[i][j];
            }
        }
    }
}