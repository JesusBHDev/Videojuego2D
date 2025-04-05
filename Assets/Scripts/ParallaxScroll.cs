using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // Ajusta según el efecto de profundidad deseado
    private Transform player;

    private void Start()
    {
        player = Camera.main.transform; // Usa la posición de la cámara como referencia
    }

    private void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = player.position.x * scrollSpeed;
        transform.position = newPosition;
    }
}
