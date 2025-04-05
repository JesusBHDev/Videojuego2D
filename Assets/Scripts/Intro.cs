using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public void intro2()
    {
        SceneManager.LoadScene("Intro2");
    }

    public void intro3()
    {
        SceneManager.LoadScene("Intro3");
        
    }

    public void intro4()
    {
        SceneManager.LoadScene("Intro4");
    }

    public void EmpezarJuego()
    {
        Destroy(GameObject.FindObjectOfType<MusicaManager>().gameObject);
        SceneManager.LoadScene("SampleScene");
    }
}
