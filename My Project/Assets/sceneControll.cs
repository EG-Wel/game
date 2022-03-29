using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneControll : MonoBehaviour
{
    public bool[] lvl;


    private void Start() => DontDestroyOnLoad(this);

    public void Scene(Scene currentScene)
    {
        for (int i = 0; i < lvl.Length; i++)
        {
            if (currentScene.name == $"Level0{i+1}")
                lvl[i] = true;
        }
    }
}
