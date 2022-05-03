using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
    public static LevelInfo instance;
    public string naam;
    public bool userExist;
    public bool password;
    public Levels[] levels;

    public Scene currentScene;

    void Start()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
            instance = this;
    }

    private void FixedUpdate() => currentScene = SceneManager.GetActiveScene();
}
