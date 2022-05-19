using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text text;
    public Text doorText;
    public int score;

    public Scene currentScene;

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void ChangeScore(int points)
    {
        // [4,8,4,5]
        if (doorText != null)
        {
            // Add points to score
            score += points;

            // Display points on display
            //text.text = score.ToString();

            // Check what door/lvl player is on
            // Display points above door

            text.text = $"{score}/{FindObjectOfType<PlayerMovement>().totalFishs}";


            /*text.text = score.ToString() + addString;*/
            doorText.text = text.text;
        }
    }
}