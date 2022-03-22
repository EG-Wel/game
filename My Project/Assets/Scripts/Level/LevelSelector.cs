using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class LevelSelector : MonoBehaviour
{
    public Canvas canvas;
    public void Select(string levelName)
    {
        UnitySceneManager.LoadScene(levelName);
        canvas.enabled = false;
    }
}
