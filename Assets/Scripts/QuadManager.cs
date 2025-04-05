using UnityEngine;

public class QuadManager : MonoBehaviour
{
    public GameObject[] quads; // Array de los Quads de fondo
    private int nextQuadIndex = 1; // Comenzamos en 1 porque el primer Quad ya está activo
    private float nextDistanceThreshold = 500f; // Primer hito en metros (cambiado a 500)
    private DistanceTracker distanceTracker;
    public Transform cameraTransform; // Referencia a la cámara principal
    public float fixedZPosition = 5f; // Posición fija en Z para los Quads, detrás de los elementos del juego

    private void Start()
    {
        distanceTracker = GameObject.FindObjectOfType<DistanceTracker>();

        // Activar el primer Quad al inicio y colocarlo en la posición de la cámara pero con Z fija
        for (int i = 0; i < quads.Length; i++)
        {
            quads[i].SetActive(i == 0); // Solo activar el primer Quad
            if (i == 0)
            {
                // Colocar el primer Quad en la posición de la cámara, pero usando una posición Z fija
                quads[i].transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, fixedZPosition);
            }
        }
    }

    private void Update()
    {
        float currentDistance = distanceTracker.GetDistanceCovered();

        // Verificación de estado para depuración
        //Debug.Log($"Distancia actual: {currentDistance}, Umbral próximo: {nextDistanceThreshold}, Índice próximo quad: {nextQuadIndex}");

        // Revisar si el personaje ha alcanzado la distancia para el próximo Quad
        if (currentDistance >= nextDistanceThreshold)
        {
            // Desactivar el quad anterior
            if (nextQuadIndex - 1 >= 0)
            {
                quads[(nextQuadIndex - 1) % quads.Length].SetActive(false);
            }

            // Activar el siguiente Quad
            StartCoroutine(TransitionQuad(quads[nextQuadIndex % quads.Length]));

            // Avanzar al próximo Quad y reiniciar ciclo si es necesario
            nextQuadIndex = (nextQuadIndex + 1) % quads.Length;
            nextDistanceThreshold += 500f; // Aumentar el umbral en 500 metros para el siguiente Quad
        }
    }

    private void LateUpdate()
    {
        // Sincronizar el primer Quad con la cámara hasta que se active el segundo Quad
        if (nextQuadIndex == 1)
        {
            quads[0].transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, fixedZPosition);
        }
    }

    private System.Collections.IEnumerator TransitionQuad(GameObject quad)
    {
        // Coloca el Quad en la misma posición X e Y que la cámara, pero con Z fija
        Vector3 cameraPosition = cameraTransform.position;
        Vector3 initialPosition = new Vector3(cameraPosition.x, cameraPosition.y, fixedZPosition);
        quad.transform.position = initialPosition;
        quad.SetActive(true);

        // Transición opcional hacia la izquierda
        float duration = 1f; // Duración de la transición en segundos
        Vector3 endPosition = initialPosition + Vector3.left * 2f; // Desplazar ligeramente hacia la izquierda
        float elapsed = 10f;

        while (elapsed < duration)
        {
            quad.transform.position = Vector3.Lerp(initialPosition, endPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Asegúrate de que el Quad esté en la posición final al terminar la transición
        quad.transform.position = endPosition;
    }
}
