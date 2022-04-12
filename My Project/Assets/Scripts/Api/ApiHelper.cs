using UnityEngine;
using System.Collections.Generic;
using System.Net;
using System.IO;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System;
using Newtonsoft.Json.Linq;


public class ApiHelper : MonoBehaviour
{
    public static ApiHelper instance;

    public Transform prefab;
    public Transform parent;

    private string GetAll = "https://localhost:7080/HighScores/GetAllUserData";

    public string naam;

    public float timeDB;

    private void Start()
    {
        if (instance is null)
            instance = this;
        naam = FindObjectOfType<LevelInfo>().name.ToLower();
        StartCoroutine(GetUserData());
    }

    public void vPrintNew() => StartCoroutine(PrintNew());

    public IEnumerator GetUserData()
    {
        print("hellofrom getuserdata");
        UnityWebRequest request = UnityWebRequest.Get(GetAll);

        yield return request.SendWebRequest();

        JSONNode userInfo = JSON.Parse(request.downloadHandler.text);

        for (int x = 0; x < userInfo.Count; x++)
        {
            string userName = userInfo[x]["name"];
            userName = userName.ToLower();
            float userTime = userInfo[x]["gameData"]["timePlayed"];
            if (userName == naam)
                timeDB = userTime;

            double time = Math.Round(userTime, 3);

            prefab.transform.Find("Placement").GetComponent<Text>().text = (x + 1).ToString();
            prefab.transform.Find("Name").GetComponent<Text>().text = userName;
            prefab.transform.Find("Time").GetComponent<Text>().text = time.ToString();

            if (userName == naam)
            {
                prefab.GetComponent<Image>().color = new Color32(245, 75, 0, 225);
                Instantiate(prefab, parent);
            }
            else
            {
                prefab.GetComponent<Image>().color = new Color32(245, 75, 0, 125);
                Instantiate(prefab, parent);
            }
        }

        if (Gongratulations.instance.level.time < timeDB)
            StartCoroutine(putTime());
    }

    public IEnumerator putTime()
    {
        string uri = "https://localhost:7080/HighScores/UpdateTimePlayed";

        string time = Gongratulations.instance.level.time.ToString();
        time = time.Replace(',','.');

        string body = String.Format("\"name\" : \"{0}\", \"time\" : {1}", naam, time);
        body = "{" + body + "}";

        byte[] myData = System.Text.Encoding.UTF8.GetBytes(body);
        UnityWebRequest www = UnityWebRequest.Put(uri, myData);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
            StartCoroutine(PrintNew());
    }

    public IEnumerator PrintNew()
    {
        foreach (Transform child in parent.transform)
            Destroy(child.gameObject);

        UnityWebRequest request = UnityWebRequest.Get(GetAll);

        yield return request.SendWebRequest();

        JSONNode userInfo = JSON.Parse(request.downloadHandler.text);

        for (int x = 0; x < userInfo.Count; x++)
        {
            string userName = userInfo[x]["name"];
            float userTime = userInfo[x]["gameData"]["timePlayed"];

            double time = Math.Round(userTime, 3);

            prefab.transform.Find("Placement").GetComponent<Text>().text = (x + 1).ToString();
            prefab.transform.Find("Name").GetComponent<Text>().text = userName;
            userName = userName.ToLower();
            prefab.transform.Find("Time").GetComponent<Text>().text = time.ToString();

            if (userName == naam)
            {
                prefab.GetComponent<Image>().color = new Color32(245, 75, 0, 225);
                Instantiate(prefab, parent);
            }
            else
            {
                prefab.GetComponent<Image>().color = new Color32(245, 75, 0, 125);
                Instantiate(prefab, parent);
            }
        }
    }
}