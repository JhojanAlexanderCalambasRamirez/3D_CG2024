//Alexander Calambas - 2190555
//Juan Manuel Santos - 2215928
//Juan David Rios - 2225674


[System.Serializable]
public class DatosJugador
{
    public string nombreJugador;
    public int puntosTotales;
    public int itemsTotales;

    // Constructor para inicializar los datos
    public DatosJugador(string nombre, int puntos, int items)
    {
        this.nombreJugador = nombre;
        this.puntosTotales = puntos;
        this.itemsTotales = items;
    }
}
