using UnityEngine;
using System.Collections.Generic;
using System.Net;
using System.IO;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Net.Http;

public class ApiHelperStart : MonoBehaviour
{
    public static ApiHelperStart instance;
    private string GetAll = "https://localhost:7080/Scores/GetAllUsers"; 
    private static readonly HttpClient client = new HttpClient();

    private void Start()
    {
        if (instance is null)
            instance = this;
    }

    public void Exists(Text name) => StartCoroutine(DoesUserExist(name.text));

    public void AddNew(Text userName) => StartCoroutine(NewUser(LevelInfo.instance.name, userName.text));

    public IEnumerator DoesUserExist(string name)
    {
        UnityWebRequest request = UnityWebRequest.Get(GetAll);

        yield return request.SendWebRequest();

        JSONNode userInfo = JSON.Parse(request.downloadHandler.text);

        for (int x = 0; x < userInfo.Count; x++)
        {
            string userName = userInfo[x]["name"];

            if (userName.ToLower() == name.ToLower())
            {
                LevelInfo.instance.userExist = true;
                break;
            }
            else
                LevelInfo.instance.userExist = false;
        }
        MainMenu.instance.PlayButton();
    }

    public IEnumerator NewUser(string name, string alias)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("alias", alias);


        UnityWebRequest www = UnityWebRequest.Post("https://localhost:7080/HighScores/AddNewUser", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
            Debug.Log(www.error);
        else
            Debug.Log("Form upload complete!");






        /*string uri = "https://localhost:7080/HighScores/AddNewUser";

        string body = String.Format("\"name\" : \"{0}\", \"alias\" : \"{1}\"", name, userName);
        body = "{" + body + "}";

        byte[] myData = System.Text.Encoding.UTF8.GetBytes(body);
        UnityWebRequest www = UnityWebRequest.Put(uri, myData);
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
        {
            LevelInfo.instance.userExist = true;
            MainMenu.instance.NewUser();
        }*/
    }
}