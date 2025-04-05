using UnityEngine.SceneManagement;
using UnityEngine;

public class controladorpersonaje : MonoBehaviour
{
    [SerializeField] public float fuerzaSalto = 1f;
    private Rigidbody2D miRigidbody2D;
    private AudioSource audioSource; // Referencia al AudioSource para el sonido de disparo

    public bool ensuelo = true;
    public Transform comprobadorsuelo;
    float comprobadorRadio = 0.07f;
    public LayerMask mascarasuelo;
    private bool estaVivo = true;
    private bool doblesalto = false;

    private Animator animator;

    private bool corriendo = false;
    public float velocidad = 1f;

    // Configuración de la bala
    public GameObject balaPrefab; // Prefab de la bala
    public Transform puntoDisparo; // Punto de disparo en el personaje
    public float velocidadBala = 10f; // Velocidad de la bala

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // Inicializa el AudioSource
        animator.SetBool("isDisparando", false);
    }

    void Start()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();

        // Encuentra el script DistanceTracker y suscribe al evento
        DistanceTracker distanceTracker = GameObject.FindObjectOfType<DistanceTracker>();
        if (distanceTracker != null)
        {
            distanceTracker.OnThresholdReached.AddListener(AumentarVelocidad);
        }
    }

    void Update()
    {
        if (!estaVivo) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Saltar();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }

        // Verificar si se deja de presionar el clic izquierdo para detener la animación de disparo
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isDisparando", false);
        }
    }

    void FixedUpdate()
    {
        // Lógica de movimiento
        if (corriendo)
        {
            miRigidbody2D.velocity = new Vector2(velocidad, miRigidbody2D.velocity.y);
        }

        // Control de animaciones
        animator.SetFloat("velx", miRigidbody2D.velocity.x);
        ensuelo = Physics2D.OverlapCircle(comprobadorsuelo.position, comprobadorRadio, mascarasuelo);
        animator.SetBool("tocarsuelo", ensuelo);

        if (ensuelo)
        {
            doblesalto = false;
        }
    }

    private void Saltar()
    {
        // Lógica para saltar
        if (corriendo)
        {
            if (ensuelo || !doblesalto)
            {
                miRigidbody2D.velocity = new Vector2(miRigidbody2D.velocity.x, fuerzaSalto);
                if (!doblesalto && !ensuelo)
                {
                    doblesalto = true;
                }
            }
        }
        else
        {
            corriendo = true;
            NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeEmpiezaACorrer");
        }
    }

    private void Disparar()
    {
        // Activa el trigger para la animación de disparo
        animator.SetTrigger("Disparar");
        animator.SetBool("isDisparando", true);

        // Reproduce el sonido de disparo
        audioSource.Play();

        // Crear la bala en el punto de disparo
        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();

        // Asignar velocidad a la bala para que se mueva en la dirección correcta
        rbBala.velocity = new Vector2(velocidadBala, 0);
    }

    // Método que se ejecuta cada vez que se alcanza un múltiplo de 600 metros
    void AumentarVelocidad()
    {
        velocidad += 1f; 
    }

    public void Morir()
    {
        if (!estaVivo) return; // Asegura que este método solo se ejecute una vez

        estaVivo = false; // Marcar al personaje como muerto
        animator.SetTrigger("Morir"); // Activar la animación de morir
        miRigidbody2D.velocity = new Vector2(0, -5f); // Ajusta el valor en Y para la velocidad de caída
        miRigidbody2D.gravityScale = 10f;

        miRigidbody2D.isKinematic = true;

        NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeHaMuerto");
        Camera.main.GetComponent<seguirpersonaje>().enabled = false;
        CargarGameOver(); }

    private void CargarGameOver()
    {
        SceneManager.LoadScene("GameOver");    }

}
