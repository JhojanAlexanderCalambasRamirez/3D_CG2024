using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    // Una variable estática para verificar si el HUD ya existe
    private static bool hudExists = false;

    void Awake()
    {
        // Si el HUD ya existe, destruye este objeto
        if (hudExists)
        {
            Destroy(gameObject);
        }
        else
        {
            // Si no existe, configúralo para que no se destruya al cambiar de escena
            DontDestroyOnLoad(gameObject);
            hudExists = true;
        }
    }
}
