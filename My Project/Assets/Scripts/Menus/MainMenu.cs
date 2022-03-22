using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class MainMenu : MonoBehaviour
{
    public bool toggle = false;

    private void Start()
    {
        //gameObject.GetComponent<AudioManager>().Play("Background"); 
        //FindObjectOfType<AudioManager>().Play("Background");
    }

    public void PlayButton()
    {
        if (toggle)
            UnitySceneManager.LoadScene("Uitleg");
        else
            UnitySceneManager.LoadScene("LevelSelector");
    }

    public void Toggle()
    {
        toggle = !toggle;
    }
}
