using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System;

public class ApiHelper : MonoBehaviour
{
    public static ApiHelper instance;

    public Transform prefab;
    public Transform parent;

    private string GetScoresByLevel = "https://localhost:7080/Scores/GetByLevel/";

    private string currentScene;
    public int currentSceneId;

    Levels[] currentlevel;

    public string naam;

    public float timeDB;

    private void Start()
    {
        if (instance is null)
            instance = this;
        currentlevel = FindObjectOfType<LevelInfo>().levels;
        foreach (var item in currentlevel)
        {
            if (item.isCurrent)
            {
                print(item.id);
                currentScene = item.sceneName;
                currentSceneId = item.id;
                FindObjectOfType<Gongratulations>().level = item;
                FindObjectOfType<Gongratulations>().Levels(item.id);
            }
        }
        naam = FindObjectOfType<LevelInfo>().naam;
        StartCoroutine(GetUserData());
        FindObjectOfType<Gongratulations>().level = currentlevel[currentSceneId];
    }

    public void vPrintNew() => StartCoroutine(RefreshHsList());

    public IEnumerator GetUserData()
    {
        UnityWebRequest request = UnityWebRequest.Get(GetScoresByLevel + currentScene);

        print(GetScoresByLevel + currentScene);

        yield return request.SendWebRequest();

        JSONNode userInfo = JSON.Parse(request.downloadHandler.text);

        print(userInfo);
        int count = 0;
        for (int x = 0; x < userInfo.Count; x++)
        {
            print(userInfo[x]);
            string userName = userInfo[x]["name"];
            float userTime = userInfo[x]["time"];

            double time = Math.Round(userTime, 3);

            prefab.transform.Find("Placement").GetComponent<Text>().text = (x + 1).ToString();
            prefab.transform.Find("Name").GetComponent<Text>().text = userName;
            prefab.transform.Find("Time").GetComponent<Text>().text = time.ToString();

            if (userName == naam)
                prefab.GetComponent<Image>().color = new Color32(245, 75, 0, 225);
            else
            {
                count++;
                prefab.GetComponent<Image>().color = new Color32(245, 75, 0, 125);
            }
                Instantiate(prefab, parent);
        }
        print(count + " => " + userInfo.Count);
        if (currentlevel[currentSceneId].DBTime == 0 || count == userInfo.Count)
            StartCoroutine(NewTime());
        

        print(currentlevel[currentSceneId].playTime + " => " + currentlevel[currentSceneId].DBTime);
        if (currentlevel[currentSceneId].playTime < currentlevel[currentSceneId].DBTime)
            StartCoroutine(putTime());
        
    }

    public IEnumerator putTime()
    {
        string uri = "https://localhost:7080/Scores/PutTimeByLevel";

        string time = Gongratulations.instance.level.playTime.ToString();
        time = time.Replace(',','.');

        string body = String.Format("\"name\" : \"{0}\", \"level\" : \"{1}\", \"time\" : {2}", naam, currentScene, time);
        body = "{" + body + "}";
        print(body);

        byte[] myData = System.Text.Encoding.UTF8.GetBytes(body);
        UnityWebRequest www = UnityWebRequest.Put(uri, myData);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);
        else
            StartCoroutine(RefreshHsList());
    }

    public IEnumerator RefreshHsList()
    {
        foreach (Transform child in parent.transform)
            Destroy(child.gameObject);

        UnityWebRequest request = UnityWebRequest.Get(GetScoresByLevel + currentScene);

        yield return request.SendWebRequest();

        JSONNode userInfo = JSON.Parse(request.downloadHandler.text);

        if (currentlevel[currentSceneId].DBTime == 0 || currentlevel[currentSceneId].playTime < currentlevel[currentSceneId].DBTime)
            currentlevel[currentSceneId].DBTime = currentlevel[currentSceneId].playTime;

        for (int x = 0; x < userInfo.Count; x++)
        {
            string userName = userInfo[x]["name"];
            float userTime = userInfo[x]["time"];
            if (userName == naam)
                timeDB = userTime;

            double time = Math.Round(userTime, 3);

            prefab.transform.Find("Placement").GetComponent<Text>().text = (x + 1).ToString();
            prefab.transform.Find("Name").GetComponent<Text>().text = userName;
            prefab.transform.Find("Time").GetComponent<Text>().text = time.ToString();

            if (userName == naam)
                prefab.GetComponent<Image>().color = new Color32(245, 75, 0, 225);
            else
                prefab.GetComponent<Image>().color = new Color32(245, 75, 0, 125);
            Instantiate(prefab, parent);
        }
    }
    public IEnumerator NewTime()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", naam);
        form.AddField("level", currentlevel[currentSceneId].sceneName);
        form.AddField("time", currentlevel[currentSceneId].playTime.ToString().Replace(',', '.'));

        print(naam);
        print(currentSceneId);
        print(currentlevel[currentSceneId].playTime.ToString());

        UnityWebRequest www = UnityWebRequest.Post("https://localhost:7080/Scores/InsertNewTimeByLevel", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);
        else
            StartCoroutine(RefreshHsList());
    }
}