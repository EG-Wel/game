using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private bool toggle = false;
    [SerializeField] private InputField name;
    public void PlayButton()
    {
        LevelInfo.instance.name = name.text;

        if (toggle)
            UnitySceneManager.LoadScene("Uitleg");
        else
            UnitySceneManager.LoadScene("LevelSelector");
    }

    public void Toggle() => toggle = !toggle;

    public void LoadScene(string levelName)
    {
        UnitySceneManager.LoadScene(levelName);
    }
}