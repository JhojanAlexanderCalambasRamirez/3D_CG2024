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

    // Referencia al controlador del menú de muerte
    public MenuMuerteController menuMuerteController;

    void Start()
    {
        // Obtener los SkinnedMeshRenderers del personaje
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

        // Notificar al MenuMuerteController cuando la salud llega a cero
        if (Salud <= 0 && menuMuerteController != null && !menuMuerteController.menuMuerte.activeSelf)
        {
            menuMuerteController.ActivarMenuMuerte();
        }

    }

    public void RecibirDaño(float daño)
    {
        Salud -= daño;
        ActualizarInterfaz();
        StartCoroutine(MostrarDaño()); // Efecto visual al recibir daño
    }

    void ActualizarInterfaz()
    {
        BarraSalud.fillAmount = Salud / SaludMaxima;
        TextoSalud.text = "+ " + Salud.ToString("f0");
    }

    IEnumerator MostrarDaño()
    {
        Color moderateRed = new Color(0.6f, 0.2f, 0.2f, 1f);

        foreach (var renderer in meshRenderers)
        {
            foreach (var material in renderer.materials)
            {
                if (material.HasProperty("_EmissionColor"))
                {
                    material.EnableKeyword("_EMISSION");
                    material.SetColor("_EmissionColor", moderateRed * 0.6f);
                }
                material.color = moderateRed;
            }
        }

        yield return new WaitForSeconds(1f);

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
