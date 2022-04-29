using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    private bool toggle = false;
    [SerializeField] private GameObject InlogCanvas;
    [SerializeField] private GameObject RegisterCanvas;
    [SerializeField] private Text name;
    [SerializeField] private InputField password;
    [SerializeField] private GameObject readOnly;

    private void Start()
    {
        if (instance is null)
            instance = this;
    }

    public void PlayButton()
    {
        if (LevelInfo.instance.userExist && LevelInfo.instance.password)
            UnitySceneManager.LoadScene("LevelSelector");
        else if (LevelInfo.instance.userExist && !LevelInfo.instance.password)
            password.GetComponent<InputField>().text = "Wrong";
        else
            LoadRegisterCanvas();
    }

    public void LoadInlogCanvas()
    {
        RegisterCanvas.SetActive(false);
        InlogCanvas.SetActive(true);
        //readOnly.GetComponent<InputField>().readOnly = false;
    }

    public void LoadRegisterCanvas()
    {
        InlogCanvas.SetActive(false);
        RegisterCanvas.SetActive(true);
        //readOnly.GetComponent<InputField>().readOnly = true;
    }

    public void NewUser() => UnitySceneManager.LoadScene("Uitleg");

    public void LoadScene(string levelName) => UnitySceneManager.LoadScene(levelName);
}