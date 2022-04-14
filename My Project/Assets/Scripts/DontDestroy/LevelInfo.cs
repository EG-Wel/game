using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
    public static LevelInfo instance;
    public string name;
    public bool userExist;
    public Level[] levels;

    public Scene currentScene;

    void Start()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
            instance = this;
    }

    private void FixedUpdate() => currentScene = SceneManager.GetActiveScene();

    public string GetCurrentScene()
    {
        if (currentScene.name == "Level01")
            return "Level01";
        else if (currentScene.name == "Level02")
            return "Level02";
        else if (currentScene.name == "Level03")
            return "Level03";
        else if (currentScene.name == "Level04")
            return "Level04";
        else
            return "MainMenu";
    }
}
