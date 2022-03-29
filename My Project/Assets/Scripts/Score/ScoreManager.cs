using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text text;
    public Text doorText;
    public int score;

    private string addString;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void ChangeScore(int points)
    {
        if (doorText != null)
        {
            // Add points to score
            score += points;

            // Display points on display
            text.text = score.ToString();

            // Check what door/lvl player is on
            // Display points above door
            if (FindObjectOfType<sceneControll>().lvl[0])
                addString = "/4";
            if (FindObjectOfType<sceneControll>().lvl[1])
                addString = "/8";
            if (FindObjectOfType<sceneControll>().lvl[2])
                addString = "/4";

            text.text = score.ToString() + addString;
            doorText.text = text.text;
        }
    }
}