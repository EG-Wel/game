using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fish : MonoBehaviour
{
    public GameObject fish;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fish.gameObject.SetActive(false);
            fish.transform.position = collision.GetComponent<PlayerMovement>().deur.transform.position;
            FindObjectOfType<AudioManager>().Play("Fish");
            ScoreManager.instance.ChangeScore(1);
        }
    }
}
