using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class ApiHelperStart : MonoBehaviour
{
    public static ApiHelperStart instance;
    private string GetAllUsers = "https://localhost:7080/Scores/GetAllUsers";
    private string GetAllLevelsByUser = "https://localhost:7080/Scores/spGetAllLevelsByUser";
    private string InsertNewUser = "https://localhost:7080/Scores/InsertNewUser";

    [Header("===GameObjects===")]
    [SerializeField] Text Name;
    [SerializeField] Text registerName;
    [SerializeField] Text Alias;
    [SerializeField] InputField Password;    
    [SerializeField] InputField registerPassword;    
    [SerializeField] Text Email;

    [Header("===Foutmeldingen===")]
    [SerializeField] private Text NameWrong;
    [SerializeField] private Text UserNameWrong;
    [SerializeField] private Text EmailWrong;
    [SerializeField] private Text PasswordShort;

    public bool register = false;

    private bool aliasExist = false;
    private bool emailExist = false;

    private void Start()
    {
        if (instance is null)
            instance = this;
    }

    public void ReadNameInput(string s) => Name.text = s.Trim();

    public void ReadregisterNameInput(string s)
    {
        registerName.text = s.Trim();
        StartCoroutine(DoesUserExist(registerName.text));
    }

    public void ReadPasswordInput(string s) => Password.text = s.Trim();

    public void ReadregisterPasswordInput(string s)
    {
        registerPassword.text = s.Trim();
        StartCoroutine(DoesUserExist(registerName.text));
    }

    public void ReadAliaswInput(string s)
    {
        Alias.text = s.Trim();
        StartCoroutine(DoesUserExist(registerName.text));
    }

    public void ReadEmailInput(string s)
    {
        Email.text = s.Trim();
        StartCoroutine(DoesUserExist(registerName.text));
    }

    public void Exists() => StartCoroutine(DoesUserExist(Name.text));

    public void AddNew()
    {
        print("ApiHelperStart => AddNew");
        StartCoroutine(DoesUserExist(registerName.text));

        if (registerName.text != "" && Email.text != "" && registerPassword.text != "" && registerPassword.text.Length >= 6 &&
            !LevelInfo.instance.userExist && !aliasExist && !emailExist)
            StartCoroutine(NewUser(registerName.text, Alias.text, Email.text, registerPassword.text));
    }

    public void refresh()
    {
        print("ApiHelperStart => refresh");

        if (LevelInfo.instance.userExist)
            NameWrong.GetComponent<Text>().enabled = true;
        else
            NameWrong.GetComponent<Text>().enabled = false;

        if (registerPassword.text.Length <= 5 && registerPassword.text.Length != 0)
            PasswordShort.GetComponent<Text>().enabled = true;
        else
            PasswordShort.GetComponent<Text>().enabled = false;

        if (aliasExist)
            UserNameWrong.GetComponent<Text>().enabled = true;
        else
            UserNameWrong.GetComponent<Text>().enabled = false;

        if (emailExist)
            EmailWrong.GetComponent<Text>().enabled = true;
        else
            EmailWrong.GetComponent<Text>().enabled = false;
    }

    public IEnumerator DoesUserExist(string name)
    {
        print("ApiHelperStart => DoesUserExist");
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

                if (register)
                {
                    for (int i = 0; i < userInfo.Count; i++)
                    {
                        string alias = userInfo[i]["alias"];

                        if (alias.ToLower() == Alias.text.ToLower())
                        {
                            aliasExist = true;
                            break;
                        }
                        else 
                            aliasExist = false;
                    }
                    for (int z = 0; z < userInfo.Count; z++)
                    {
                        string email = userInfo[z]["email"];

                        if (email.ToLower() == Email.text.ToLower())
                        {
                            emailExist = true;
                            break;
                        }
                        else 
                            emailExist = false;
                    }
                    refresh();
                }
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
        print("ApiHelperStart => NewUser");
        StartCoroutine(DoesUserExist(name));

        if (!LevelInfo.instance.userExist)
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
        print("ApiHelperStart => GetLevelByUserName");

        string uri = GetAllLevelsByUser + name;
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