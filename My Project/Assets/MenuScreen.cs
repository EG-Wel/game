using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    public GameOverScreen restartGame;
    public GameObject canvas;
    public Button btnReturn, btnSettings, btnRestart, btnQuitGame;

    // Start is called before the first frame update
    void Start()
    {
        btnReturn.onClick.AddListener(OnClickReturn);
        btnSettings.onClick.AddListener(OnClickSettings);
        btnRestart.onClick.AddListener(OnClickRestart);
        btnQuitGame.onClick.AddListener(OnClickQuitGame);
    }

    void OnClickReturn()
    {
        Time.timeScale = 1;
        FindObjectOfType<PlayerMovement>().menuActive = false;
        canvas.SetActive(false);
    }
    void OnClickSettings()
    {
        /// nothing yet
    }
    void OnClickRestart()
    {
        restartGame.RestartButton();
        Time.timeScale = 1;
    }
    void OnClickQuitGame()
    {
        Application.Quit();
    }
}
