using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [Header("===Non Random Fields===")]
    [SerializeField] private int speed = 5;
    [SerializeField] private int distance = 5;

    [SerializeField] private PhysicsMaterial2D wallMaterial;
    [SerializeField] private PhysicsMaterial2D moving;

    private int direction;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        RandomStartDir();    
    }

    // Update is called once per frame
    void Update() => Move();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<PlayerMovement>().isJumping)
            collision.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = moving;
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<PlayerMovement>().isJumping)
            collision.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = moving;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = wallMaterial;
    }

    private void RandomStartDir()
    {
        direction = Random.Range(1, 3);
        if (direction == 1)
            rb.velocity = new Vector2(speed, 0);
        else
            rb.velocity = new Vector2(-speed, 0);
    }

    private void Move()
    {
        if (rb.position.x < startPos.x - distance)
            rb.velocity = new Vector2(speed, 0);
        if (rb.position.x > startPos.x + distance)
            rb.velocity = new Vector2(-speed, 0);
    }
}
