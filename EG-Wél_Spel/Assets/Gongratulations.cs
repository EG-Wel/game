using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gongratulations : MonoBehaviour
{
    public static Gongratulations instance;
    public int currentScene;
    public Levels level;

    [SerializeField] Text yourTime;

    [SerializeField] GameObject[] stars;
    [SerializeField] GameObject[] cupIcon;
    [SerializeField] GameObject[] lockIcon;
    [SerializeField] GameObject[] tTBText;
    [SerializeField] Button[] buttons;

    public string lvlNext;
    public string lvlPrev;

    void Start()
    {
        if (instance is null)
            instance = this;
    }

    void Update() => yourTime.text = FormatTime(level.playTime);

    public void Levels(int currentLevel)
    {
        string[] Level01Times = { "0:10" , "0:15" , "0:30" };
        string[] Level02Times = { "0:30" , "0:45" , "0:60" };
        string[] Level03Times = { "0:15" , "0:25" , "0:45" };
        string[] Level04Times = { "0:45" , "0:60" , "1:30" };


        if (currentLevel == 0)
        {
            for (int i = 0; i < Level01Times.Length; i++)
                tTBText[i].GetComponent<Text>().text = Level01Times[i];

            lvlNext = $"Level0{currentLevel + 2}";
            lvlPrev = $"Level0{currentLevel + 1}";
            if (level.playTime < 30f)
                putStars(0);
            if (level.playTime < 15f)
                putStars(1);
            if (level.playTime < 10f)
                putStars(2);
        }
        if (currentLevel == 1)
        {
            for (int i = 0; i < Level02Times.Length; i++)
                tTBText[i].GetComponent<Text>().text = Level02Times[i];

            lvlNext = $"Level0{currentLevel + 2}";
            lvlPrev = $"Level0{currentLevel + 1}";
            if (level.playTime < 60f)
                putStars(0);
            if (level.playTime < 45f)
                putStars(1);
            if (level.playTime < 30f)
                putStars(2);
        }
        if (currentLevel == 2)
        {
            for (int i = 0; i < Level03Times.Length; i++)
                tTBText[i].GetComponent<Text>().text = Level03Times[i];

            lvlNext = $"Level0{currentLevel + 2}";
            lvlPrev = $"Level0{currentLevel + 1}";
            if (level.playTime < 45f)
                putStars(0);
            if (level.playTime < 25f)
                putStars(1);
            if (level.playTime < 15f)
                putStars(2);
        }
        if (currentLevel == 3)
        {
            for (int i = 0; i < Level04Times.Length; i++)
                tTBText[i].GetComponent<Text>().text = Level04Times[i];

            lvlNext = $"Level0{currentLevel + 2}";
            lvlPrev = $"Level0{currentLevel + 1}";
            if (level.playTime < 45f)
                putStars(0);
            if (level.playTime < 60f)
                putStars(1);
            if (level.playTime < 75f)
                putStars(2);
        }
    }

    private void putStars(int value)
    {
        lockIcon[value].SetActive(false);
        cupIcon[value].SetActive(true);
        for (int x = 0; x < value+1; x++)
            stars[x].SetActive(true);
    }

    public string FormatTime(float time) => Math.Round(time, 3).ToString();

    public void LoadScene(string welke)
    {
        if (welke == "next")
            SceneManager.LoadScene(lvlNext);
        else if (welke == "prev") 
            SceneManager.LoadScene(lvlPrev);
    }
}
