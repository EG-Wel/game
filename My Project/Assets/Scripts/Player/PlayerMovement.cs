using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public GameObject menuScreen;
    public CharacterController2D controller;
    public Animator animator;
    public Vector3 door2;
    public Vector3 spawn;
    public CircleCollider2D circleCollider;
    public Cinemachine.CinemachineVirtualCamera cam;

    float coyoteTime = 0.15f;
    public float coyoteTimeCounter;

    public float runSpeed = 40f;
    public bool isJumping = false;
    public bool stopMovement = false;
    public bool jump = false;
    public bool facingRight = true;
    public bool floor = false;
    public bool menuActive = false;
    //bool noseOnFloor = false;
    public bool movingRight;
    public bool movingLeft;

    float horizontalMove = 0f;
    private void Start()
    {
        UnitySceneManager.Scene currentScene = UnitySceneManager.SceneManager.GetActiveScene();
        FindObjectOfType<sceneControll>().Scene(currentScene);
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        Rigidbody2D playerRb = GetComponent<Rigidbody2D>();

        //PrintVelocoty();

        SetIsJumping();
       
        //MovePlayer();

        MovingDirection();

        CoyoteTime(playerRb);

        WalkingAnim();

        JumpingAnim();

        DraggUpdate(playerRb);

        WallBuggFix();

        MenuScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoorDetection"))
            ScoreManager.instance.Door(collision);
        if (collision.gameObject.CompareTag("Void"))
        {
            collision.gameObject.GetComponent<Canvas>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoorDetection"))
            ScoreManager.instance.Door(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
            CheckDoor(collision);

        /*if (collision.gameObject.layer == 8)
        {
            controller.GetComponent<Rigidbody2D>().gravityScale = 2;
            stopMovement = true;
        }*/
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

    

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, false, jump);
        jump = false;
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
                UnitySceneManager.SceneManager.LoadScene("Level02");
                controller.transform.SetPositionAndRotation(spawn, Quaternion.identity);
                facingRight = true;
                floor = false;
                cam.m_Lens.OrthographicSize = 8;
                cam.m_Lens.NearClipPlane = -20;
                FindObjectOfType<AudioManager>().Play("Door");
            }
        }

        if (collision.gameObject.name == "Door_2-3")
        {
            if (ScoreManager.instance.score >= 8)
            {
                UnitySceneManager.SceneManager.LoadScene("Level03");
                controller.transform.SetPositionAndRotation(spawn, Quaternion.identity);
                facingRight = true;
                floor = false;
                FindObjectOfType<AudioManager>().Play("Door");
            }
        }

        if (collision.gameObject.name == "Door_3-4")
        {
            if (ScoreManager.instance.score >= 4)
            {
                UnitySceneManager.SceneManager.LoadScene("Level04");
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
    void DraggUpdate(Rigidbody2D rb)
    {
        // When player falling drag increases so it looks like he is gliding
        if (rb.velocity.y < 0)
            rb.drag = 5f;
        else
            rb.drag = 0f;

        if (!isJumping)
            rb.drag = 0f;
    }

    void WalkingAnim()
    {
        // If player is moving -> play running animation
        // Else stop animation
        if (horizontalMove != 0)
            animator.SetFloat("IsRunning", 1f);
        else
            animator.SetFloat("IsRunning", 0f);
    }
    void SetIsJumping()
    {
        isJumping = !GetComponent<CharacterController2D>().m_Grounded;
    }

    void JumpingAnim()
    {
        if (FindObjectOfType<CharacterController2D>().m_Grounded)
        {
            animator.SetBool("IsJumping", false);
            //FindObjectOfType<AudioManager>().Play("Jump");
        }
        else
            animator.SetBool("IsJumping", true);
    }

    void CoyoteTime(Rigidbody2D rb)
    {
        if (GetComponent<CharacterController2D>().m_Grounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

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

        if (rb.velocity.y > 20)
            if (GetComponent<CharacterController2D>().m_JumpForce == 650)
                rb.velocity = new Vector2(0, 10);
            else if (GetComponent<CharacterController2D>().m_JumpForce == 850)
                rb.velocity = new Vector2(0, 14);
    }

    void MovingDirection()
    {

    }

    void MovePlayer()
    {
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
    }

    void PrintVelocoty()
    {
        print(controller.GetComponent<Rigidbody2D>().velocity.y);
    }

    void MenuScreen()
    {
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
    }










    void WallBuggFix()
    {
        
    }
}
