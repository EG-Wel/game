using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour
{
    public GameObject[] Levels;
    public bool[] lvl;

    void Start() => lvl = FindObjectOfType<sceneControll>().lvl;

    private void FixedUpdate()
    {
        for (int i = 0; i < lvl.Length; i++)
        {
            if (lvl[i])
                Levels[i].GetComponent<Button>().interactable = true;
            if (!lvl[i])
                Levels[i].GetComponent<Button>().interactable = false;
        }
    }
}
