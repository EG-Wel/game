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
            if (doorText.name == "Aantal_Fish_lvl1")
                doorText.text = score.ToString() + "/4";

            if (doorText.name == "Aantal_Fish_lvl2")
                doorText.text = score.ToString() + "/8";

            if (doorText.name == "Aantal_Fish_lvl3")
                doorText.text = score.ToString() + "/4";
        }
    }

    public void Door(Collider2D collider)
    {
        if (collider.gameObject.name == "Door")
            doorText = collider.gameObject.GetComponent<DoorText>().text;

        ChangeScore(0);
    }
}