using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text text;
    public Text doorText;
    public int score;

    private int lvl1 = 0;
    private int lvl2 = 1;
    private int lvl3 = 2;
    private int lvl4 = 3;


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
            if (sceneControll.instance.lvl[lvl1])
                addString = "/4";
            if (sceneControll.instance.lvl[lvl2])
                addString = "/8";
            if (sceneControll.instance.lvl[lvl3])
                addString = "/4";
            if (sceneControll.instance.lvl[lvl4])
                addString = "/5";

            text.text = score.ToString() + addString;
            doorText.text = text.text;
            print(score);
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