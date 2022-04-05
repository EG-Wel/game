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
            fish.transform.position = collision.GetComponent<PlayerMovement>().deur.transform.position;
            FindObjectOfType<AudioManager>().Play("Fish");
            fish.gameObject.SetActive(false);
            ScoreManager.instance.ChangeScore(1);
        }
    }
}
