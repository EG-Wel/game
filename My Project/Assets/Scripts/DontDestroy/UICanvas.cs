using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UICanvas : MonoBehaviour
{
    public static UICanvas instance;
    public GameObject[] Levels;
    bool[] lvl;

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    private void FixedUpdate()
    {
        lvl = sceneControll.instance.lvl;
        lvl[0] = true;
        for (int i = 0; i < lvl.Length; i++)
        {
            if (lvl[i])
                Levels[i].GetComponent<Button>().interactable = true;
            if (!lvl[i])
                Levels[i].GetComponent<Button>().interactable = false;
        }
    }

    
}
