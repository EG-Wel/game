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
    [SerializeField] private Text name;
    [SerializeField] private GameObject readOnly;

    private void Start()
    {
        if (instance is null)
            instance = this;
    }

    public void PlayButton()
    {
        LevelInfo.instance.name = name.text;

        if (LevelInfo.instance.userExist)
            UnitySceneManager.LoadScene("LevelSelector");
        else
        {
            nameCanvas.SetActive(false);
            newUserCanvas.SetActive(true);
            readOnly.GetComponent<InputField>().readOnly = true;
        }
    }

    public void NewUser() => UnitySceneManager.LoadScene("Uitleg");

    public void Toggle() => toggle = !toggle;

    public void LoadScene(string levelName) => UnitySceneManager.LoadScene(levelName);
}