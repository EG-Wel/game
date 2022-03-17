using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject menuScreen;
    public CharacterController2D controller;
    public Animator animator;
    public Vector3 door2;
    public Vector3 spawn;
    public CircleCollider2D circleCollider;

    float coyoteTime = 0.15f;
    public float coyoteTimeCounter;

    public float runSpeed = 40f;
    public bool isJumping = false;
    public bool stopMovement = false;
    public bool jump = false;
    public bool facingRight = true;
    public bool floor = false;
    public bool menuActive = false;
    bool noseOnFloor = false;

    float horizontalMove = 0f;
    
    // Update is called once per frame
    void Update()
    {
        print(controller.GetComponent<Rigidbody2D>().velocity.y);
        isJumping = !GetComponent<CharacterController2D>().m_Grounded;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (GetComponent<CharacterController2D>().m_Grounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        // If player is moving -> play running animation
        // Else stop animation
        if (horizontalMove != 0)
            animator.SetFloat("IsRunning", 1f);
        else
            animator.SetFloat("IsRunning", 0f);

        if (Input.GetButtonDown("Jump"))
        {
            if (coyoteTimeCounter > 0f)
            {
                jump = true;
                coyoteTimeCounter = 0f;
            }
            else
                jump = false;
        }

        // When player falling drag increases so it looks like he is gliding
        if (rb.velocity.y < 0)
            rb.drag = 5f;
        else
            rb.drag = 0f;

        if (!isJumping)
            rb.drag = 0f;

        if (horizontalMove != 0 || jump)
        {
            if (!stopMovement)
            {
                controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
                jump = false;
            }
        }
            
        if (horizontalMove == 0)
            controller.Move(0, false, jump);

        if (controller.GetComponent<Rigidbody2D>().velocity.y > 20)
            if (GetComponent<CharacterController2D>().m_JumpForce == 650)
                controller.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
            else if (GetComponent<CharacterController2D>().m_JumpForce == 850)
                controller.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 14);
        
        if (FindObjectOfType<CharacterController2D>().m_Grounded)
        {
            animator.SetBool("IsJumping", false);
            FindObjectOfType<AudioManager>().Play("Jump");
        }
        else
            animator.SetBool("IsJumping", true);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuActive)
            {
                menuScreen.SetActive(false);
                menuActive = false;
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
                menuScreen.SetActive(true);
                menuActive = true;
            }
        }

        if (!FindObjectOfType<CharacterController2D>().m_Grounded && floor)
        {
            if (noseOnFloor && facingRight)
                controller.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
            else if (noseOnFloor && !facingRight)
                controller.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
            else if (facingRight && floor)
                controller.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
            else if (!facingRight && floor)
                controller.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            noseOnFloor = true;

        if (collision.gameObject.CompareTag("DoorDetection"))
            ScoreManager.instance.Door(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
            noseOnFloor = false;

        if (collision.gameObject.CompareTag("DoorDetection"))
            ScoreManager.instance.Door(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
            floor = true;
        }

        if (collision.gameObject.CompareTag("Door"))
            CheckDoor(collision);

        if (collision.gameObject.CompareTag("Wall"))
            stopMovement = true;

        if (collision.gameObject.layer == 8)
        {
            controller.GetComponent<Rigidbody2D>().gravityScale = 2;
            stopMovement = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
            stopMovement = false;

        if (collision.gameObject.CompareTag("Floor"))
            floor = false;
    }

    

    private void FixedUpdate()
    {
        // if player is moving but facing different direction -> Flip player
        if (horizontalMove < 0 && facingRight)
            flip();
        else if (horizontalMove > 0 && !facingRight)
            flip();
    }

    void CheckDoor(Collision2D collision)
    {
        if (collision.gameObject.name == "Door_1-2")
        {
            if (ScoreManager.instance.score >= 4)
            {
                GetComponent<CharacterController2D>().m_JumpForce = 850f;
                SceneManager.LoadScene(2);
                controller.transform.SetPositionAndRotation(spawn, Quaternion.identity);
                facingRight = true;
                floor = false;
                FindObjectOfType<AudioManager>().Play("Door");
            }
        }

        if (collision.gameObject.name == "Door_2-3")
        {
            if (ScoreManager.instance.score >= 12)
            {
                SceneManager.LoadScene(3);
                controller.transform.SetPositionAndRotation(spawn, Quaternion.identity);
                facingRight = true;
                floor = false;
                FindObjectOfType<AudioManager>().Play("Door");
            }
        }

        if (collision.gameObject.name == "Door_3-4")
        {
            if (ScoreManager.instance.score >= 16)
            {
                SceneManager.LoadScene(4);
                controller.transform.SetPositionAndRotation(spawn, Quaternion.identity);
                facingRight = true;
                floor = false;
                collision.gameObject.GetComponent<AudioSource>().PlayDelayed(3f);
                FindObjectOfType<AudioManager>().Play("RUN!");
                FindObjectOfType<AudioManager>().Stop("Background");
                FindObjectOfType<AudioManager>().Play("Door");
            }
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0);
    }
}
