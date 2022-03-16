using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishWalrus : MonoBehaviour
{
    public GameObject fish;
    public GameObject walrus;

    //float speed = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(fish);
            collision.GetComponent<PlayerMovement>().runSpeed = collision.GetComponent<PlayerMovement>().runSpeed + 5;
            walrus.GetComponent<walrusMove>().speed += 1.2f;
        }
    }
}
