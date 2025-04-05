using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] obj;
    public float tiempomin = 1f;
    public float tiempomax = 2f;

    // Start is called before the first frame update
    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeEmpiezaACorrer");

    }

    void PersonajeEmpiezaACorrer (Notification notificacion)
    {
        Generar();
    }

    void Update()
    {

    }

    void Generar()
    {
        // Instanciar el objeto en la posición actual pero con Z = 0
        Vector3 posicion = new Vector3(transform.position.x, transform.position.y, 0);
        Instantiate(obj[Random.Range(0, obj.Length)], posicion, Quaternion.identity);

        // Invocar de nuevo el método Generar después de un tiempo aleatorio
        Invoke("Generar", Random.Range(tiempomin, tiempomax));
    }
}
