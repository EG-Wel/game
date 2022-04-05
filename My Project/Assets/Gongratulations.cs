using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gongratulations : MonoBehaviour
{
    //Scene currentScene;
    public int currentScene;
    Level level;

    [SerializeField] Text yourTime;

    [SerializeField] GameObject[] stars;
    [SerializeField] GameObject[] cupIcon;
    [SerializeField] GameObject[] lockIcon;

    void Start()
    {
        level = LevelInfo.instance.levels[0];
    }

    void Update()
    {
        double mainGameTimerd = (double)level.time;
        TimeSpan time = TimeSpan.FromSeconds(mainGameTimerd);
        string displayTime = time.ToString("mm':'ss");

        yourTime.text = displayTime;

        Levels();
    }

    private void Levels()
    {
        if (currentScene == 1)
        {
            if (level.time < 50f)
            {
                lockIcon[0].SetActive(false);
                cupIcon[0].SetActive(true);
                stars[0].SetActive(true);
            }
            if (level.time < 25f)
            {
                lockIcon[1].SetActive(false);
                cupIcon[1].SetActive(true);
                for (int x = 0; x < 2; x++)
                    stars[x].SetActive(true);
            }
            if (level.time < 10f)
            {
                lockIcon[2].SetActive(false);
                cupIcon[2].SetActive(true);
                for (int x = 0; x < 3; x++)
                    stars[x].SetActive(true);
            }
        }
        if (currentScene == 2)
        {
            if (level.time < 60f)
            {
                lockIcon[0].SetActive(false);
                cupIcon[0].SetActive(true);
                stars[0].SetActive(true);
            }
            if (level.time < 45f)
            {
                lockIcon[1].SetActive(false);
                cupIcon[1].SetActive(true);
                for (int x = 0; x < 2; x++)
                    stars[x].SetActive(true);
            }
            if (level.time < 30f)
            {
                lockIcon[2].SetActive(false);
                cupIcon[2].SetActive(true);
                for (int x = 0; x < 3; x++)
                    stars[x].SetActive(true);
            }
        }
        if (currentScene == 3)
        {
            if (level.time < 60f)
            {
                lockIcon[0].SetActive(false);
                cupIcon[0].SetActive(true);
                stars[0].SetActive(true);
            }
            if (level.time < 30f)
            {
                lockIcon[1].SetActive(false);
                cupIcon[1].SetActive(true);
                for (int x = 0; x < 2; x++)
                    stars[x].SetActive(true);
            }
            if (level.time < 15f)
            {
                lockIcon[2].SetActive(false);
                cupIcon[2].SetActive(true);
                for (int x = 0; x < 3; x++)
                    stars[x].SetActive(true);
            }
        }
    }

    public string FormatTime(float time)
    {
        int minutes = (int)time / 60000;
        int seconds = (int)time / 1000 - 60 * minutes;
        int milliseconds = (int)time - minutes * 60000 - 1000 * seconds;
        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
