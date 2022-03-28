using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class UICanvas : MonoBehaviour
{
    public GameObject[] Levels;
    public GameObject Level01;
    public GameObject Level02;
    public GameObject Level03;
    public GameObject Level04;
    public Scene currentScene;

    public int hearts;
    public bool scene02 = false;
    public bool scene03 = false;
    public bool scene04 = false;

    void Start()
    {
        scene02 = FindObjectOfType<sceneControll>().s2;
        scene03 = FindObjectOfType<sceneControll>().s3;
        scene04 = FindObjectOfType<sceneControll>().s4;
    }
    private void FixedUpdate()
    {
        currentScene = UnitySceneManager.GetActiveScene();

        if (currentScene.name == "Level02")
            scene02 = true;
        if (currentScene.name == "Level03")
            scene03 = true;
        if (currentScene.name == "Level04")
            scene04 = true;

        if (currentScene.name == "LevelSelector")
        {
            GetComponent<Canvas>().enabled = true;
            if (scene02)
                Levels[1].GetComponent<Button>().interactable = true;
            if (scene03)
                Level03.GetComponent<Button>().interactable = true;
            if (scene04)
                Level04.GetComponent<Button>().interactable = true;     
        }
    }
}
