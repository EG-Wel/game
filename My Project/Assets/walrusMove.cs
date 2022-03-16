using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walrusMove : MonoBehaviour
{
    public float speed = 50;
    public Transform Walrus;
    public Transform Player;
    public GameObject edgeColl;

    Rigidbody2D rb;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Invoke("MoveWalrus", 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(Player.position, Walrus.position);
        if (rb.position.y > 0)
            rb.velocity = new Vector2(speed, -10);
        else if (rb.position.y < -0.8)
            rb.velocity = new Vector2(speed, 10);
        if (Player.position.x < Walrus.position.x)
            speed = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Walrus.position = new Vector2(-40, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Player = collision.transform;
    }

    public void MoveWalrus()
    {
        rb.velocity = new Vector2(speed, 1);
        Invoke("DestroyWall", 1.5f);
    }

    public void DestroyWall() => Destroy(edgeColl);
}
