using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool toggle = false;
    public void PlayButton()
    {
        if (toggle)
            SceneManager.LoadScene("Uitleg");
        else
            SceneManager.LoadScene("LevelSelector");
    }

    public void Toggle()
    {
        toggle = !toggle;
    }
}
