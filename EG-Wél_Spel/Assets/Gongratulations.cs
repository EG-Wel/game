using System;
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

    private string lvlNext;
    private string lvlPrev;

    public Times[] lvlTimes;

    void Start()
    {
        if (instance is null)
            instance = this;
    }

    void Update() => yourTime.text = FormatTime(level.playTime);

    public void Levels(int currentLevel)
    {
        for (int i = 0; i < lvlTimes.Length; i++)
        {
            if (level.sceneName == lvlTimes[i].levelName)
            {
                for (int x = 0; x < 3; x++)
                    tTBText[x].GetComponent<Text>().text = lvlTimes[i].timeString[x];

                lvlNext = $"Level0{currentLevel + 2}";
                lvlPrev = $"Level0{currentLevel + 1}";

                int g = 0;
                for (int c = 0; c < 3; c++)
                {
                    if (level.playTime < lvlTimes[i].timeInt[c])
                        g++;
                }
                putStars(g);
            }
        }
    }

    private void putStars(int value)
    {
        for (int i = 0; i < value; i++)
        {
            lockIcon[i].SetActive(false);
            cupIcon[i].SetActive(true);
            stars[i].SetActive(true);
        }
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
