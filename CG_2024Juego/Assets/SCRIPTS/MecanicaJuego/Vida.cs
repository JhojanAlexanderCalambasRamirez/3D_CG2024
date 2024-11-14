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

    public Animator animator;
    private SkinnedMeshRenderer[] meshRenderers;
    private List<Color[]> originalColors = new List<Color[]>();

    public MenuMuerteController menuMuerteController;
    public bool muertePorJefe = false;
    public bool muertePorEnemigo = false;

    void Start()
    {
        if (menuMuerteController == null)
        {
            Debug.LogError("MenuMuerteController no está asignado en el Inspector");
            return;
        }

        // Cargar el valor de salud desde PlayerPrefs si existe
        if (PlayerPrefs.HasKey("SaludPersistente"))
        {
            Salud = PlayerPrefs.GetFloat("SaludPersistente");
        }

        // Limitar la salud al valor máximo
        Salud = Mathf.Clamp(Salud, 0, SaludMaxima);

        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
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

        if (Salud <= 0 && menuMuerteController != null && !menuMuerteController.panelDead.activeSelf)
        {
            if (muertePorJefe)
            {
                animator.Play("JefeMeMata");
            }
            else if (muertePorEnemigo)
            {
                animator.Play("EnemigosMeMata");
            }

            StartCoroutine(OnDeath());
        }
    }

    public void RecibirCura(float cura)
    {
        Salud = Mathf.Min(Salud + cura, SaludMaxima);
        GuardarSalud();
    }

    public void RecibirDaño(float daño, bool esAtaqueJefe = false)
    {
        Salud -= daño;
        ActualizarInterfaz();
        StartCoroutine(MostrarDaño());

        if (Salud <= 0)
        {
            if (esAtaqueJefe)
            {
                animator.Play("JefeMeMata");
            }
            else
            {
                animator.Play("EnemigosMeMata");
            }

            StartCoroutine(OnDeath());
        }

        GuardarSalud();
    }

    private IEnumerator OnDeath()
    {
        // Espera un pequeño retraso antes de activar el menú de muerte
        yield return new WaitForSeconds(0.5f);
        menuMuerteController.ActivarMenuMuerte();
    }

    void ActualizarInterfaz()
    {
        if (BarraSalud != null)
        {
            BarraSalud.fillAmount = Salud / SaludMaxima;
        }

        if (TextoSalud != null)
        {
            TextoSalud.text = "+ " + Salud.ToString("f0");
        }
    }

    public void RegistrarMuerte()
    {
        int muertes = PlayerPrefs.GetInt("muertesJugador", 0) + 1;
        PlayerPrefs.SetInt("muertesJugador", muertes);
    }

    private IEnumerator MostrarDaño()
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

    // Método para guardar la salud en PlayerPrefs
    private void GuardarSalud()
    {
        PlayerPrefs.SetFloat("SaludPersistente", Salud);
        PlayerPrefs.Save();
    }
}
