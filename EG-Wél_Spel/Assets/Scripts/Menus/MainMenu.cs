using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    [SerializeField] private GameObject InlogCanvas;
    [SerializeField] private GameObject RegisterCanvas;
    [SerializeField] private Text naam;
    [SerializeField] private InputField password;
    [SerializeField] private GameObject readOnly;

    [Header("===Foutmeldingen===")]
    [SerializeField] private Text wrong;


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
            wrong.GetComponent<Text>().text = "Wrong password";
        else
            wrong.GetComponent<Text>().text = "User does not exists";
    }

    public void LoadInlogCanvas()
    {
        ApiHelperStart.instance.register = false;
        RegisterCanvas.SetActive(false);
        InlogCanvas.SetActive(true);
    }

    public void LoadRegisterCanvas()
    {
        ApiHelperStart.instance.register = true;
        LevelInfo.instance.naam = string.Empty;
        LevelInfo.instance.userExist = false;
        InlogCanvas.SetActive(false);
        RegisterCanvas.SetActive(true);
    }

    public void ServerUnsucces() => wrong.GetComponent<Text>().text = "Couldn't connect to database";

    public void NewUser() => UnitySceneManager.LoadScene("Uitleg");

    public void LoadScene(string levelName) => UnitySceneManager.LoadScene(levelName);
}