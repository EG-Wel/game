using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneControll : MonoBehaviour
{
    public bool s2 = false;
    public bool s3 = false;
    public bool s4 = false;

    private void Start() => DontDestroyOnLoad(this);

    public void Scene(Scene currentScene)
    {
        print(currentScene.name);
        if (currentScene.name == "Level02")
            s2 = true;
        if (currentScene.name == "Level03")
            s3 = true;
        if (currentScene.name == "Level04")
            s4 = true;
    }

    public void Scene2()
    {

    }

    public void Scene3()
    {

    }

    public void Scene4()
    {

    }
}
