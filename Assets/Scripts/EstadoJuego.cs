using System.IO;
using UnityEngine;

public class EstadoJuego : MonoBehaviour
{
    public int puntuacionMaxima = 0;
    public int puntajeActual = 0;
    public static EstadoJuego estadojuego;
    private string rutaArchivo;

    private void Awake()
    {
        rutaArchivo = Application.persistentDataPath + "/datos.json";
        if (estadojuego == null)
        {
            estadojuego = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (estadojuego != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Cargar();
      
    }

    public void Guardar()
    {
        DatosAguardar datos = new DatosAguardar();
        datos.puntuacionMaxima = puntuacionMaxima;

        string json = JsonUtility.ToJson(datos);
        File.WriteAllText(rutaArchivo, json);
    }

    public void Cargar()
    {
        if (File.Exists(rutaArchivo))
        {
            string json = File.ReadAllText(rutaArchivo);
            DatosAguardar datos = JsonUtility.FromJson<DatosAguardar>(json);
            puntuacionMaxima = datos.puntuacionMaxima;
        }
        else
        {
            puntuacionMaxima = 0;
        }
    }
    public void ReiniciarMejorPuntuacion()
    {
        puntuacionMaxima = 0; // Reinicia la puntuación máxima
        Guardar(); // Guarda el cambio en el archivo
    }

}

[System.Serializable]
class DatosAguardar
{
    public int puntuacionMaxima;
}
