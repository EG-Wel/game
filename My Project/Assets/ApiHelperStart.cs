using UnityEngine;
using System.Collections.Generic;
using System.Net;
using System.IO;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class ApiHelperStart : MonoBehaviour
{
    public static ApiHelperStart instance;
    private string GetAll = "https://localhost:7080/HighScores/GetAllUserData";
    public Text name;

    private void Start()
    {
        if (instance is null)
            instance = this;
    }

    public void Exists() => StartCoroutine(DoesUserExist());

    public IEnumerator DoesUserExist()
    {
        UnityWebRequest request = UnityWebRequest.Get(GetAll);

        yield return request.SendWebRequest();

        JSONNode userInfo = JSON.Parse(request.downloadHandler.text);

        for (int x = 0; x < userInfo.Count; x++)
        {
            string userName = userInfo[x]["name"];

            if (userName == name.text)
            {
                LevelInfo.instance.userExist = true;
                break;
            }
            else
                LevelInfo.instance.userExist = false;
        }
        MainMenu.instance.PlayButton();
    }
}