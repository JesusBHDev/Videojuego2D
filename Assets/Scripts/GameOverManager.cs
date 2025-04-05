using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI puntajeActualText;
    public TextMeshProUGUI mejorPuntajeText;


    void OnEnable()
    {
        // Obtén los puntajes desde EstadoJuego
        int puntajeActual = EstadoJuego.estadojuego.puntajeActual;
        int mejorPuntaje = EstadoJuego.estadojuego.puntuacionMaxima;

        // Actualiza los textos
        puntajeActualText.text = "Tu Puntaje: " + puntajeActual;
        mejorPuntajeText.text = "Mejor Puntaje: " + mejorPuntaje;
    }


    public void ReiniciarJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void IrAlMenuPrincipal()
    {
       SceneManager.LoadScene("MenuPrincipal");
    }
}
