using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretRoom : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Secret"))
        {
            Debug.Log("HEllo wordk");
            SceneManager.LoadScene(0);
        }
    }
}
