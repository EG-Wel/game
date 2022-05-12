using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;

public class UICanvas : MonoBehaviour
{
    public static UICanvas instance;
    public GameObject[] Levels;
    bool[] lvl;

    void Start()
    {
        if (instance == null)
            instance = this;
        StartCoroutine(GetLevelByUserName(FindObjectOfType<LevelInfo>().naam));
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

    public IEnumerator GetLevelByUserName(string name)
    {
        print("UICanvas => GetLevelByUserName");

        string uri = "https://localhost:7080/Scores/spGetAllLevelsByUser" + name;
        UnityWebRequest request = UnityWebRequest.Get(uri);

        yield return request.SendWebRequest();

        JSONNode userInfo = JSON.Parse(request.downloadHandler.text);

        for (int x = 0; x < userInfo.Count; x++)
        {
            string levelName = userInfo[x]["name"];
            for (int i = 0; i < FindObjectOfType<LevelInfo>().levels.Length; i++)
            {
                if (FindObjectOfType<LevelInfo>().levels[i].sceneName == levelName)
                    FindObjectOfType<sceneControll>().lvl[i + 1] = true;
            }
        }
    }
}
