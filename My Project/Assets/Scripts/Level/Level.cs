using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Level 
{
    public string scene;

    public int currentScene;

    public bool isCurrent;

    public bool completed;
    
    public float time;
}
