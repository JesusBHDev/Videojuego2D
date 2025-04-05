using UnityEngine;
using UnityEngine.Events;

public class DistanceTracker : MonoBehaviour
{
    public TextMesh distanceText;
    [SerializeField] public float distanceCovered = 0f;
    private Vector3 lastPosition;

    public UnityEvent OnThresholdReached; // Evento para notificar cuando se alcanza un umbral de 600 metros

    private float nextThreshold = 400f; // Umbral inicial

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        distanceCovered += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        distanceText.text = "Distancia: " + Mathf.FloorToInt(distanceCovered).ToString() + " m";

        // Verifica si hemos alcanzado o superado el umbral de 600 metros
        if (distanceCovered >= nextThreshold)
        {
            OnThresholdReached?.Invoke(); // Invoca el evento
            nextThreshold += 400f; // Actualiza el próximo umbral
        }
    }

    public float GetDistanceCovered()
    {
        return distanceCovered;
    }
}
