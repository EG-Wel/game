using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameOverScreen restartGame;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Button btnReturn, btnSettings, btnRestart, btnQuitGame;

    void Start()
    {
        player.SetActive(false);
        btnReturn.onClick.AddListener(OnClickReturn);
        btnSettings.onClick.AddListener(OnClickSettings);
        btnRestart.onClick.AddListener(OnClickRestart);
        btnQuitGame.onClick.AddListener(OnClickQuitGame);
    }

    void OnClickReturn()
    {
        player.SetActive(true);
        Time.timeScale = 1;
        FindObjectOfType<PlayerMovement>().menuActive = false;
        canvas.SetActive(false);
    }

    void OnClickSettings()
    {
        restartGame.RestartButton();
        Time.timeScale = 1;
    }

    void OnClickRestart()
    {
        for (int i = 0; i < LevelInfo.instance.levels.Length; i++)
        {
            if (LevelInfo.instance.levels[i].isCurrent)
                SceneManager.LoadScene(LevelInfo.instance.levels[i].sceneName);
        }
        Time.timeScale = 1;
    }

    void OnClickQuitGame() => Application.Quit();
}