using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBird : MonoBehaviour
{
    [SerializeField] private Rigidbody2D birdRigidbody2d;

    [Header("===Non Random Fields===")]
    [SerializeField] private int speed = 5;
    [SerializeField] private int distance = 5;
    
    [Header("===Random===")]
    [SerializeField] private bool useRandomSpeed;
    [SerializeField] private bool useRandomDistance;

    [Header ("===Random Speed Values===")]
    [SerializeField] private int randomSpeedMin = 3;
    [SerializeField] private int randomSpeedMax = 11;

    [Header ("===Random Distance Values===")]
    [SerializeField] private int randomDistanceMin = 1;
    [SerializeField] private int randomDistanceMax = 6;

    private int direction;
    private Vector2 startPos;
    private bool facingRight = true;

    void Start()
    {
        startPos = birdRigidbody2d.position;
        direction = Random.Range(1, 3);

        if (useRandomSpeed)
        {
            speed = Random.Range(randomSpeedMin, randomSpeedMax);
           
            if (direction == 1)
                birdRigidbody2d.velocity = new Vector2(speed, 0);
            else
            {
                birdRigidbody2d.velocity = new Vector2(-speed, 0);
                Flip();
            }
        }
        else
            birdRigidbody2d.velocity = new Vector2(speed, 0);
        if (useRandomDistance)
            distance = Random.Range(randomDistanceMin, randomDistanceMax);
    }

    void Update()
    {
        if (birdRigidbody2d.position.x < startPos.x - distance && !facingRight)
        {
            birdRigidbody2d.velocity = new Vector2(speed, 0);
            Flip();
        }
        if (birdRigidbody2d.position.x > startPos.x + distance && facingRight)
        {
            birdRigidbody2d.velocity = new Vector2(-speed, 0);
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        gameObject.GetComponent<SpriteRenderer>().flipX = !facingRight;
    }
}
