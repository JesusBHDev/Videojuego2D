using UnityEngine;

public class QuadManager : MonoBehaviour
{
    public GameObject[] quads; // Array de los Quads de fondo
    private int nextQuadIndex = 1; // Comenzamos en 1 porque el primer Quad ya est� activo
    private float nextDistanceThreshold = 500f; // Primer hito en metros (cambiado a 500)
    private DistanceTracker distanceTracker;
    public Transform cameraTransform; // Referencia a la c�mara principal
    public float fixedZPosition = 5f; // Posici�n fija en Z para los Quads, detr�s de los elementos del juego

    private void Start()
    {
        distanceTracker = GameObject.FindObjectOfType<DistanceTracker>();

        // Activar el primer Quad al inicio y colocarlo en la posici�n de la c�mara pero con Z fija
        for (int i = 0; i < quads.Length; i++)
        {
            quads[i].SetActive(i == 0); // Solo activar el primer Quad
            if (i == 0)
            {
                // Colocar el primer Quad en la posici�n de la c�mara, pero usando una posici�n Z fija
                quads[i].transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, fixedZPosition);
            }
        }
    }

    private void Update()
    {
        float currentDistance = distanceTracker.GetDistanceCovered();

        // Verificaci�n de estado para depuraci�n
        //Debug.Log($"Distancia actual: {currentDistance}, Umbral pr�ximo: {nextDistanceThreshold}, �ndice pr�ximo quad: {nextQuadIndex}");

        // Revisar si el personaje ha alcanzado la distancia para el pr�ximo Quad
        if (currentDistance >= nextDistanceThreshold)
        {
            // Desactivar el quad anterior
            if (nextQuadIndex - 1 >= 0)
            {
                quads[(nextQuadIndex - 1) % quads.Length].SetActive(false);
            }

            // Activar el siguiente Quad
            StartCoroutine(TransitionQuad(quads[nextQuadIndex % quads.Length]));

            // Avanzar al pr�ximo Quad y reiniciar ciclo si es necesario
            nextQuadIndex = (nextQuadIndex + 1) % quads.Length;
            nextDistanceThreshold += 500f; // Aumentar el umbral en 500 metros para el siguiente Quad
        }
    }

    private void LateUpdate()
    {
        // Sincronizar el primer Quad con la c�mara hasta que se active el segundo Quad
        if (nextQuadIndex == 1)
        {
            quads[0].transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, fixedZPosition);
        }
    }

    private System.Collections.IEnumerator TransitionQuad(GameObject quad)
    {
        // Coloca el Quad en la misma posici�n X e Y que la c�mara, pero con Z fija
        Vector3 cameraPosition = cameraTransform.position;
        Vector3 initialPosition = new Vector3(cameraPosition.x, cameraPosition.y, fixedZPosition);
        quad.transform.position = initialPosition;
        quad.SetActive(true);

        // Transici�n opcional hacia la izquierda
        float duration = 1f; // Duraci�n de la transici�n en segundos
        Vector3 endPosition = initialPosition + Vector3.left * 2f; // Desplazar ligeramente hacia la izquierda
        float elapsed = 10f;

        while (elapsed < duration)
        {
            quad.transform.position = Vector3.Lerp(initialPosition, endPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Aseg�rate de que el Quad est� en la posici�n final al terminar la transici�n
        quad.transform.position = endPosition;
    }
}
