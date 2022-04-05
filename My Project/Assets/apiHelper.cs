using UnityEngine;
using System.Collections.Generic;
using System.Net;
using System.IO;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class apiHelper : MonoBehaviour
{
    public static apiHelper instance;

    public Transform prefab;
    public Transform parent;

    private void Start()
    {
        if (instance is null)
            instance = this;
    }

    IEnumerator GetUserData()
    {
        /*HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://localhost:7080/HighScores/SearchByName/Stan");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<UserData>(json);*/


        UnityWebRequest request = UnityWebRequest.Get("https://localhost:7080/HighScores/SearchByName/Stan");

        yield return request.SendWebRequest();

        JSONNode userInfo = JSON.Parse(request.downloadHandler.text);

        string userName = userInfo["name"];

        prefab.GetComponentInChildren<Text>().text = $"{userName}";
        Instantiate(prefab, parent);
    }
}