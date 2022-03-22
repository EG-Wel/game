using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class GameOverScreen : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject overlay;
    public Text pointsText;
    public void Setup(int score)
    {
        overlay.SetActive(false);
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " Fish";
    }

    public void RestartButton()
    {
        Destroy(Canvas);
        UnitySceneManager.LoadScene("LevelSelector");
    }
}
