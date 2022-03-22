using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Canvas canvas;
    public void Select(string levelName)
    {
        SceneManager.LoadScene(levelName);
        canvas.enabled = false;
    }
}
