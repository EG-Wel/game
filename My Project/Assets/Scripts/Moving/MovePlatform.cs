using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("===Non Random Fields===")]
    public int speed = 5;
    public int distance = 5;

    [Header("===Random===")]
    public bool useRandomSpeed;
    public bool useRandomDistance;

    [Header ("===Random Speed Values===")]
    public int randomSpeedMin = 3;
    public int randomSpeedMax = 11;

    [Header ("===Random Distance Values===")]
    public int randomDistanceMin = 1;
    public int randomDistanceMax = 6;

    int direction;
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        RandomStartDir();    
    }


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<PlayerMovement>().isJumping)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 10;
            collision.gameObject.GetComponent<CharacterController2D>().m_JumpForce = 950;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
            collision.gameObject.GetComponent<CharacterController2D>().m_JumpForce = 850;
        }
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
