using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class MainMenu : MonoBehaviour
{
    private bool toggle = true;

    public void PlayButton()
    {
        if (toggle)
            UnitySceneManager.LoadScene("Uitleg");
        else
            UnitySceneManager.LoadScene("LevelSelector");
    }

    public void Toggle() => toggle = !toggle;

    public void LoadScene(string levelName)
    {
        if (levelName == "Back")
        {

        }
        else if(levelName == "Next")
        {
            
        }
        else
            UnitySceneManager.LoadScene(levelName);
    }
}