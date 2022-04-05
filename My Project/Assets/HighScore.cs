using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public HighScore instance;
    public Transform prefab;
    public Transform parent;

    void Start()
    {
        if (instance is null)
            instance = this;
        /*for (int i = 1; i < 100; i++)
        {
            prefab.GetComponentInChildren<Text>().text = $"{i}";
            Instantiate(prefab, parent);
        }*/
    }

    public void NewUserData()
    {
        /*IEnumerator hi = 
*/      /*      ApiHelper.GetUserData();
        print("");*/

        /*prefab.GetComponentInChildren<Text>().text = $"{userData.Name}";
        Instantiate(prefab, parent);*/
    }
}
