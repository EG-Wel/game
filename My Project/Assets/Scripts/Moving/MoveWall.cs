using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;

    [Header("===Non Random Fields===")]
    public int speed = 5;
    public int distance = 5;
    public bool randDirectionOnStart;

    int direction;
    Vector2 startPos;

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
