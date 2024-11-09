using UnityEngine;

public class CogerArmas : MonoBehaviour
{
    public GameObject[] armas; // Array de GameObjects de las armas en la mano

    public void RecogerArma(int index)
    {
        armas[index].SetActive(true);
    }

    public void ActivarArmar(int index)
    {
        DesactivarArmas(); // Desactiva cualquier otra arma activa antes de activar la seleccionada
        armas[index].SetActive(true);
    }

    public void DesactivarArmas()
    {
        foreach (var arma in armas)
        {
            arma.SetActive(false);
        }
    }
}
