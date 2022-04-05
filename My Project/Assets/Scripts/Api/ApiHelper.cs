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

    private string GetByName = "https://localhost:7080/HighScores/SearchByName/";
    private string GetAll = "https://localhost:7080/HighScores/GetAllUserData";

    public string naam;

    public float timeDB;

    public string[] names = { "Stan", "Boyd", "Emily", "Demo", "Ans"};

    private void Start()
    {
        /*names[0] = "Stan";
        names[1] = "Boyd";
        names[2] = "Emily";
        names[3] = "Demo";
        names[4] = "Ans";*/

        naam = FindObjectOfType<LevelInfo>().name;
        if (instance is null)
            instance = this;
        GetByName += naam;
        StartCoroutine(GetUserData());
        //GetUserTime();

        
    }

    public IEnumerator GetUserData()
    {
        UnityWebRequest request = UnityWebRequest.Get(GetAll);

        yield return request.SendWebRequest();

        JSONNode userInfo = JSON.Parse(request.downloadHandler.text);

        for (int x = 0; x < userInfo.Count; x++)
        {
            print(userInfo.GetType());

            print(userInfo);
            string userName = userInfo[x]["name"];
            float userTime = userInfo[x]["gameData"]["timePlayed"];
            if (userName == naam)
                timeDB = userTime;
            print(timeDB);

            string time = Gongratulations.instance.FormatTime(userTime);

            prefab.transform.Find("Placement").GetComponent<Text>().text = (x + 1).ToString();
            prefab.transform.Find("Name").GetComponent<Text>().text = userName;
            prefab.transform.Find("Time").GetComponent<Text>().text = time;

            Instantiate(prefab, parent);
        }

        print(naam + "   " + Gongratulations.instance.level.time.ToString() + "   " + timeDB.ToString());
        if (Gongratulations.instance.level.time < timeDB)
            StartCoroutine(putTime());
        
    }

    public IEnumerator putTime()
    {
        string uri = "https://localhost:7080/HighScores/UpdateTimePlayed";
        /*print("updated");
        byte[] data = System.Text.Encoding.UTF8.GetBytes("");
        *//*WWWForm data = new WWWForm();
        data.AddField("name", name);
        data.AddField("time", Gongratulations.instance.level.time.ToString());*//*
        using (UnityWebRequest request = UnityWebRequest.Put(uri, data))
        {
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log("Upload complete!");
            }
        }*/
        //string template = ;
        print(naam);
        print(Gongratulations.instance.level.time);
        string time = Gongratulations.instance.level.time.ToString();
        time = time.Replace(',','.');
        print(time);

        string body = String.Format("\"name\" : \"{0}\", \"time\" : {1}", naam, time);
        body = "{" + body + "}";
        print(body);

        byte[] myData = System.Text.Encoding.UTF8.GetBytes(body);
        UnityWebRequest www = UnityWebRequest.Put(uri, myData);
        www.SetRequestHeader("Content-Type", "application/json");
        print(www.url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Upload complete!");
            foreach (Transform child in parent.transform)
            {
                child.gameObject.SetActive(false);
            }
            StartCoroutine(GetUserData());
        }
    }

    public IEnumerator GetUserTime()
    {
        UnityWebRequest request = UnityWebRequest.Get(GetByName);

        yield return request.SendWebRequest();

        JSONNode userInfo = JSON.Parse(request.downloadHandler.text);
        
        print(userInfo["gameData"]["timePlayed"]);
    }
}