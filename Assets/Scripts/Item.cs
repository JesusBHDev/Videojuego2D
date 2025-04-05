using UnityEngine;

public class Item : MonoBehaviour
{
    public int puntosGanados = 2;
    private AudioSource audioSource;
    private Collider2D itemCollider2D; // Renombrado
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Obtiene el AudioSource, Collider y SpriteRenderer del objeto
        audioSource = GetComponent<AudioSource>();
        itemCollider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            // Reproduce el sonido de la moneda
            audioSource.Play();

            // Notifica para incrementar puntos
            NotificationCenter.DefaultCenter().PostNotification(this, "IncrementarPuntos", puntosGanados);

            // Desactiva el collider y el sprite renderer para que la moneda desaparezca visualmente y no colisione más
            itemCollider2D.enabled = false; // Usando el nuevo nombre
            spriteRenderer.enabled = false;

            // Destruye el objeto después de que el sonido ha terminado de reproducirse
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
