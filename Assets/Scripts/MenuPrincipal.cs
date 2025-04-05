using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuPrincipal : MonoBehaviour
{
    public TextMeshProUGUI mejorPuntajeText;

    private void OnEnable()
    {
        if (EstadoJuego.estadojuego != null)
        {
            int mejorPuntaje = EstadoJuego.estadojuego.puntuacionMaxima;
            mejorPuntajeText.text = "Mejor Puntaje: " + mejorPuntaje;
        }
        else
        {
            mejorPuntajeText.text = "Mejor Puntaje: 0"; 
        }
    }



    public void resetear()
    {
        EstadoJuego.estadojuego.ReiniciarMejorPuntuacion();
        mejorPuntajeText.text = "Mejor Puntaje: " + EstadoJuego.estadojuego.puntuacionMaxima;
    }

    public void intro1()
    {
        SceneManager.LoadScene("Intro1");
    }

    public void SalirDelJuego()
    {
        Application.Quit(); // Cierra la aplicación cuando se ejecuta fuera del editor.
    }
}
