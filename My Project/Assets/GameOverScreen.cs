using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene("LevelSelector");
    }
}
