using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gongratulations : MonoBehaviour
{
    public static Gongratulations instance;
    //Scene currentScene;
    public int currentScene;
    public Level level;

    [SerializeField] Text yourTime;

    [SerializeField] GameObject[] stars;
    [SerializeField] GameObject[] cupIcon;
    [SerializeField] GameObject[] lockIcon;

    void Start()
    {
        if (instance is null)
        {
            instance = this;
        }
        level = LevelInfo.instance.levels[0];
    }

    void Update()
    {
        yourTime.text = FormatTime(level.time);

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
        
        double mainGameTimerd = (double)time;
        TimeSpan times = TimeSpan.FromSeconds(mainGameTimerd);
        return times.ToString("m':'ss");
    }
}
