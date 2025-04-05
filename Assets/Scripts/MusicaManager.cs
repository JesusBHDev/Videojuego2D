using UnityEngine;

public class MusicaManager : MonoBehaviour
{
    private static MusicaManager instancia;

    private void Awake()
    {
        // Si ya existe una instancia, destruye el objeto duplicado
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        // Asigna esta instancia como la única y no destruirla entre escenas
        instancia = this;
        DontDestroyOnLoad(gameObject);
    }
}
