using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puntuacion : MonoBehaviour
{
    public int puntuacion = 0;
    public TextMesh marcador;

    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "IncrementarPuntos");
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeHaMuerto");

        ActualizarMarcador();
    }
    void IncrementarPuntos (Notification notificacion)
    {
        int puntosAIncrementar = (int)notificacion.data;
        puntuacion += puntosAIncrementar;
        ActualizarMarcador();
    }
    void PersonajeHaMuerto(Notification notificacion)
    {
 
        EstadoJuego.estadojuego.puntajeActual = puntuacion;

        if (puntuacion > EstadoJuego.estadojuego.puntuacionMaxima)
        {
           EstadoJuego.estadojuego.puntuacionMaxima = puntuacion;
            EstadoJuego.estadojuego.Guardar();
        }
        else
        {
            Debug.Log("record no superado!!! Maxima: " + EstadoJuego.estadojuego.puntuacionMaxima + " Actual: " + puntuacion);
        }
    }




    void ActualizarMarcador()
    {
        marcador.text = "Puntaje: " + puntuacion.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
