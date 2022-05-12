using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    [SerializeField] private GameObject InlogCanvas;
    [SerializeField] private GameObject RegisterCanvas;
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
        print("MainMenu => PlayButton");
        if (LevelInfo.instance.userExist && LevelInfo.instance.password)
            UnitySceneManager.LoadScene("LevelSelector");
        else if (LevelInfo.instance.userExist && !LevelInfo.instance.password)
            wrong.GetComponent<Text>().text = "Wrong password";
        else
            wrong.GetComponent<Text>().text = "User does not exists";
    }

    public void LoadInlogCanvas()
    {
        print("MainMenu => LoadInlogCanvas");
        ApiHelperStart.instance.register = false;
        RegisterCanvas.SetActive(false);
        InlogCanvas.SetActive(true);
    }

    public void LoadRegisterCanvas()
    {
        print("MainMenu => LoadRegisterCanvas");
        ApiHelperStart.instance.register = true;
        LevelInfo.instance.naam = string.Empty;
        LevelInfo.instance.userExist = false;
        InlogCanvas.SetActive(false);
        RegisterCanvas.SetActive(true);
    }

    public void ServerUnsucces()
    {
        print("MainMenu => ServerUnsucces");
        wrong.GetComponent<Text>().text = "Cannot connect to destination host";
    }

    public void NewUser()
    {
        print("MainMenu => NewUser");
        UnitySceneManager.LoadScene("Uitleg");
    }

    public void LoadScene(string levelName)
    {
        print("MainMenu => LoadScene");
        UnitySceneManager.LoadScene(levelName);
    }
}