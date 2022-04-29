using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2d;

    [Header("===Non Random Fields===")]
    [SerializeField] private int speed = 5;
    [SerializeField] private int distance = 5;
    [SerializeField] private bool randDirectionOnStart;

    private int direction;
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = rigidbody2d.position;
        if (randDirectionOnStart)
        {
            direction = Random.Range(1, 3);
            if (direction == 1)
                rigidbody2d.velocity = new Vector2(0, speed);
            else
                rigidbody2d.velocity = new Vector2(0, -speed);
        }
        else
            rigidbody2d.velocity = new Vector2(0, speed);        
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidbody2d.position.y < startPos.y - distance)
            rigidbody2d.velocity = new Vector2(0, speed);

        if (rigidbody2d.position.y > startPos.y + distance)
            rigidbody2d.velocity = new Vector2(0, -speed);
    }
}
