using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class keep : MonoBehaviour
{ 
    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;
    public GameObject Level4;

    public Scene currentScene;
    
    public int hearts;

    public bool scene1 = false;
    public bool scene2 = false;
    public bool scene3 = false;
    public bool scene4 = false;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void FixedUpdate()
    {
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Level1")
            scene1 = true;
        if (currentScene.name == "Level2")
            scene2 = true;
        if (currentScene.name == "Level3")
            scene3 = true;
        if (currentScene.name == "Level4")
            scene4 = true;

        if (currentScene.name == "LevelSelector")
        {
            GetComponent<Canvas>().enabled = true;
            if (scene1)
                Level1.GetComponent<Button>().interactable = true;
            if (scene2)
                Level2.GetComponent<Button>().interactable = true;
            if (scene3)
                Level3.GetComponent<Button>().interactable = true;
            if (scene4)
                Level4.GetComponent<Button>().interactable = true;
        }
    }
}
