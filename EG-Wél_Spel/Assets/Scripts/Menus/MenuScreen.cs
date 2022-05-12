using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameOverScreen restartGame;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Button btnReturn, btnSettings, btnRestart, btnQuitGame;

    void Start() => player.GetComponent<PlayerDeath>().minimap.enabled = false;

    public void OnClickReturn()
    {
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<PlayerDeath>().minimap.enabled = true;
        FindObjectOfType<PlayerMovement>().scoreCanvas.SetActive(true);
        FindObjectOfType<PlayerMovement>().menuActive = false;
        canvas.SetActive(false);
    }

    public void OnClickLevels()
    {
        restartGame.RestartButton();
    }

    public void OnClickRestart()
    {
        for (int i = 0; i < LevelInfo.instance.levels.Length; i++)
        {
            if (LevelInfo.instance.levels[i].isCurrent)
                SceneManager.LoadScene(LevelInfo.instance.levels[i].sceneName);
        }
    }

    public void OnClickQuitGame() => Application.Quit();
    public void OnClickTime(int time) => Time.timeScale = time;
}