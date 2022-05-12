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

    private string addString;

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

    public bool Enough(Scene scene)
    {
        if (scene.name == "Level01" && doorText.text == "4/4")
            return true;
        else if (scene.name == "Level02" && doorText.text == "8/8")
            return true;
        else if (scene.name == "Level03" && doorText.text == "4/4")
            return true;
        else if (scene.name == "Level04" && doorText.text == "5/5")
            return true;
        else
            return false;
    }
}