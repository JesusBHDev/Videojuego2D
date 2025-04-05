using UnityEngine;

public class EnemigoAI : MonoBehaviour
{
    public Transform jugador; // Referencia al transform del jugador
    public float distanciaDeteccion = 5f; // Distancia de detección
    public float tiempoParado = 1f; // Tiempo de espera antes de correr
    public float velocidad = 2f; // Velocidad de movimiento
    public float margenVertical = 0.5f; // Margen en el eje Y

    public int balasParaMorir = 3; // Cantidad de balas necesarias para morir (editable en Inspector)
    private int balasRecibidas = 0; // Cantidad de balas recibidas

    private Animator animator;
    private AudioSource audioSource;
    private bool persiguiendo = false;
    private bool estaVivo = true;
    private bool atacando = false;
    private bool haAtacadoAlIniciar = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        Invoke("IniciarAtaqueYCorrer", tiempoParado);
    }

    private void Update()
    {
        if (persiguiendo && estaVivo && !atacando && haAtacadoAlIniciar)
        {
            Vector2 objetivo = new Vector2(jugador.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, objetivo, velocidad * Time.deltaTime);

            float distanciaAlJugadorX = Mathf.Abs(transform.position.x - jugador.position.x);
            float distanciaAlJugadorY = Mathf.Abs(transform.position.y - jugador.position.y);

            if (distanciaAlJugadorX <= 0.5f && distanciaAlJugadorY <= margenVertical)
            {
                DetenerJuego();
            }
        }
    }

    private void IniciarAtaqueYCorrer()
    {
        if (!haAtacadoAlIniciar)
        {
            animator.SetTrigger("Atacar");
            haAtacadoAlIniciar = true;
            Invoke("EmpezarACorrer", 0.5f);
        }
    }

    private void EmpezarACorrer()
    {
        persiguiendo = true;
        animator.SetTrigger("EmpezarCorrer");

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (!estaVivo) return;

        if (colision.CompareTag("Player"))
        {
            controladorpersonaje personaje = colision.GetComponent<controladorpersonaje>();
            if (personaje != null)
            {
                personaje.Morir();
                NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeHaMuerto");
            }
        }
        else if (colision.CompareTag("Bala"))
        {
            RecibirDisparo();
            Destroy(colision.gameObject);
        }
    }

    private void RecibirDisparo()
    {
        balasRecibidas++;
        if (balasRecibidas >= balasParaMorir)
        {
            Morir();
        }
    }

    private void DetenerJuego()
    {
        Debug.Log("El enemigo ha alcanzado al jugador. El juego se detiene.");
        Time.timeScale = 0;
    }

    private void Morir()
    {
        estaVivo = false;
        animator.SetTrigger("Morir");
        Destroy(gameObject, 1f);
    }
}
