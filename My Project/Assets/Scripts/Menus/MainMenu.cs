using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    private bool toggle = false;
    [SerializeField] private GameObject nameCanvas;
    [SerializeField] private GameObject newUserCanvas;

    private void Start()
    {
        if (instance is null)
            instance = this;
    }
    public void PlayButton()
    {
        LevelInfo.instance.name = ApiHelperStart.instance.name.text;

        if (LevelInfo.instance.userExist)
            UnitySceneManager.LoadScene("LevelSelector");
        else
        {
            nameCanvas.SetActive(false);
            newUserCanvas.SetActive(true);
        }
    }

    public void AddUser()
    {

    }

    public void Toggle() => toggle = !toggle;

    public void LoadScene(string levelName) => UnitySceneManager.LoadScene(levelName);
}