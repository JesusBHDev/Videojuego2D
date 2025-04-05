using UnityEngine.SceneManagement;
using UnityEngine;

public class Destructor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D colision)
    {
        if(colision.tag == "Player")
        {
            controladorpersonaje personaje = colision.GetComponent<controladorpersonaje>();
            NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeHaMuerto");
            personaje.Morir();
            CargarGameOver();
        }
        else
        {
            Destroy(colision.gameObject);
        }
      // Detener el tiempo despu�s de 1 segundo (ajusta seg�n la duraci�n de la animaci�n)
    }

    private void CargarGameOver()
    {
        SceneManager.LoadScene("GameOver");// Cambia "GameOver" al nombre exacto de tu escena de Game Over
    }
}
