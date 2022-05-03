using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using System.Net.Http;

public class ApiHelperStart : MonoBehaviour
{
    public static ApiHelperStart instance;
    private string GetAllUsers = "https://localhost:7080/Scores/GetAllUsers";
    private string GetAllLevelsByUser = "https://localhost:7080/Scores/spGetAllLevelsByUser";
    private string InsertNewUser = "https://localhost:7080/Scores/InsertNewUser";
    private static readonly HttpClient client = new HttpClient();

    [Header("===GameObjects===")]
    [SerializeField] Text Name;
    [SerializeField] Text registerName;
    [SerializeField] Text Alias;
    [SerializeField] InputField Password;    
    [SerializeField] InputField registerPassword;    
    [SerializeField] Text Email;    

    private void Start()
    {
        if (instance is null)
            instance = this;
    }

    public void ReadNameInput(string s) => Name.text = s.Trim();
    public void ReadregisterNameInput(string s) => registerName.text = s.Trim();
    public void ReadregisterPasswordInput(string s) => registerPassword.text = s.Trim();
    public void ReadAliaswInput(string s) => Alias.text = s.Trim();
    public void ReadPasswordInput(string s) => Password.text = s.Trim();
    public void ReadEmailInput(string s) => Email.text = s.Trim();

    public void Exists() => StartCoroutine(DoesUserExist(Name.text));

    public void AddNew() => StartCoroutine(NewUser(registerName.text, Alias.text, Email.text, registerPassword.text));
    
    public IEnumerator DoesUserExist(string name)
    {
        UnityWebRequest request = UnityWebRequest.Get(GetAllUsers);

        yield return request.SendWebRequest();

        JSONNode userInfo = JSON.Parse(request.downloadHandler.text);

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            MainMenu.instance.ServerUnsucces();
        }
        else
        { 
            for (int x = 0; x < userInfo.Count; x++)
            {
                string userName = userInfo[x]["name"];
                string password = userInfo[x]["password"];

                if (userName == name)
                {
                    LevelInfo.instance.naam = userName;
                    LevelInfo.instance.userExist = true;
                    if (password == Password.text)
                    {
                        LevelInfo.instance.password = true;
                        StartCoroutine(GetLevelByUserName(name));
                    }
                    else
                        LevelInfo.instance.password = false;
                    break;
                }
                else
                    LevelInfo.instance.userExist = false;

                if (name == "")
                    LevelInfo.instance.userExist = false;
            }
                    MainMenu.instance.PlayButton();
        }
    }

    public IEnumerator NewUser(string name, string alias, string email, string password)
    {
        print(name);
        StartCoroutine(DoesUserExist(name));

        if (LevelInfo.instance.userExist == false)
        {
            WWWForm form = new WWWForm();
            form.AddField("name", name);
            form.AddField("alias", alias);
            form.AddField("email", email);
            form.AddField("password", password);


            UnityWebRequest www = UnityWebRequest.Post(InsertNewUser, form);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) 
                Debug.Log(www.error);
            else
            {
                LevelInfo.instance.userExist = true;
                MainMenu.instance.PlayButton();
                MainMenu.instance.LoadInlogCanvas();
            }
        }
        else print($"Gebruiker {name} bestaat al");
    }

    public IEnumerator GetLevelByUserName(string name)
    {
        string uri = GetAllLevelsByUser + name;
        print(uri);
        UnityWebRequest request = UnityWebRequest.Get(uri);

        yield return request.SendWebRequest();

        JSONNode userInfo = JSON.Parse(request.downloadHandler.text);

        for (int x = 0; x < userInfo.Count; x++)
        {
            string levelName = userInfo[x]["name"];
            float time = userInfo[x]["time"];

            for (int i = 0; i < LevelInfo.instance.levels.Length; i++)
            {
                if (LevelInfo.instance.levels[i].sceneName == levelName)
                {
                    LevelInfo.instance.levels[i].DBTime = time;
                    sceneControll.instance.lvl[i+1] = true;
                }
            }
        }
        MainMenu.instance.PlayButton();
    }
}