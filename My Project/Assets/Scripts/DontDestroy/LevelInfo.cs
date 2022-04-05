using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
    public static LevelInfo instance;
    public Level[] levels;

    public Scene currentScene;

    void Start()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        print(currentScene.name);
    }

    public string GetCurrentScene()
    {
        if (currentScene.name == "Level01")
            return "Level01";
        if (currentScene.name == "Level02")
            return "Level02";
        if (currentScene.name == "Level03")
            return "Level03";
        if (currentScene.name == "Level04")
            return "Level04";
        else
            return "MainMenu";
    }
}
