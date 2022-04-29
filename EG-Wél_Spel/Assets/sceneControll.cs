using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneControll : MonoBehaviour
{
    public static sceneControll instance;
    public bool[] lvl = new bool[15];

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void Scene(Scene currentScene)
    {
        for (int i = 0; i < lvl.Length; i++)
        {
            if (currentScene.name == $"Level0{i + 1}")
                lvl[i] = true;
        }
    }
}