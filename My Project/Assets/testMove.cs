/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMove : MonoBehaviour
{
    public aaaTest controller;
    float horizontalMove;
    bool jump = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
            jump = true;
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove, false, jump);
        jump = false;
    }
}
*/using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement;


public class testMove : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public GameObject menuScreen;
    public aaaTest controller;
    public Animator animator;
    public Vector3 door2;
    public Vector3 spawn;
    public CircleCollider2D circleCollider;
    public Cinemachine.CinemachineVirtualCamera cam;
    public Transform rotate;
    public Canvas canvasDeur;

    float coyoteTime = 0.15f;
    public float coyoteTimeCounter;

    public float runSpeed = 40f;
    public bool isJumping = false;
    public bool stopMovement = false;
    public bool facingRight = true;
    public bool floor = false;
    public bool menuActive = false;
    public bool movingRight;
    public bool movingLeft;
    public float jumpTime;

    public float jumpCounter;
    float horizontalMove = 0f;
    bool jump = false;

    private void Start()
    {
        UnitySceneManager.Scene currentScene = UnitySceneManager.SceneManager.GetActiveScene();
        //FindObjectOfType<sceneControll>().Scene(currentScene);
    }

    void Update()
    {
        playerRb = GetComponent<Rigidbody2D>();

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        /*isGrounded = GetComponent<CharacterController2D>().m_Grounded;

        JumpingAnim();

        SetIsJumping();

        CoyoteTime();

        WalkingAnim();*/

        DraggUpdate(playerRb);

        MenuScreen();

        HoldJump();

        Dive();

        if (Input.GetButtonDown("Jump"))
            jump = true;
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, false, jump);
        jump = false;

        if (horizontalMove > 0 && !facingRight)
            flip();

        if (horizontalMove < 0 && facingRight)
            flip();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
            CheckDoor(collision);
    }

    void CheckDoor(Collision2D collision)
    {
        if (collision.gameObject.name == "Door_1-2")
        {
            if (ScoreManager.instance.score >= 4)
            {
                UnitySceneManager.SceneManager.LoadScene("Level02");
                controller.transform.SetPositionAndRotation(spawn, Quaternion.identity);
                facingRight = true;
                floor = false;
                cam.m_Lens.OrthographicSize = 8;
                cam.m_Lens.NearClipPlane = -20;
                FindObjectOfType<AudioManager>().Play("Door");
            }
            else
                canvasDeur.GetComponent<Canvas>().enabled = true;
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
                FindObjectOfType<AudioManager>().Play("Door");
            }
        }
    }

    void flip()
    {
        /*transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);*/
        /*Vector3 currentScale = rotate.transform.localScale;
        currentScale.x = currentScale.x * -1;
        rotate.transform.localScale = currentScale;
        facingRight = !facingRight;*/
        /*if (playerRb.velocity.x > 0)
            gameObject.transform.Rotate(0f, 180f, 0f);
        else if (playerRb.velocity.x < 0)
            gameObject.transform.Rotate(0f, 0f, 0f);*/
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

    // If player is moving -> play running animation
    // Else stop animation
    void WalkingAnim()
    {
        if (horizontalMove != 0)
            animator.SetFloat("IsRunning", 1f);
        else
            animator.SetFloat("IsRunning", 0f);
    }


    void SetIsJumping() => isJumping = !GetComponent<CharacterController2D>().m_Grounded;

    // Wanneer player jumped show jump animation
    void JumpingAnim()
    {
        if (FindObjectOfType<CharacterController2D>().m_Grounded)
            animator.SetBool("IsJumping", false);
        else
            animator.SetBool("IsJumping", true);
    }

    // CoyoteTime
    void CoyoteTime()
    {
        if (GetComponent<CharacterController2D>().m_Grounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;
    }

    // Wanneer user esc knop klikt paused de game en andersom
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

    // Wanneer de user jump button ingehoud drukt 
    // springt de player hoger
    void HoldJump()
    {
        if (!isJumping && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpCounter = jumpTime;
            playerRb.velocity = new Vector2(playerRb.velocity.x, GetComponent<CharacterController2D>().m_JumpForce);
        }

        jumpCounter -= Time.deltaTime;

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpCounter > 0)
                playerRb.velocity = new Vector2(playerRb.velocity.x, GetComponent<CharacterController2D>().m_JumpForce);
            else
                isJumping = false;
        }

        if (Input.GetButtonUp("Jump"))
            isJumping = false;
    }

    void Dive()
    {
        if (Input.GetButtonDown("Down") && isJumping)
            playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y - 1f);
        if (Input.GetButton("Down") && isJumping)
        {
            playerRb.drag = 0f;
            animator.SetBool("IsDiving", true);
        }
        if (Input.GetButtonUp("Down"))
            animator.SetBool("IsDiving", false);
    }
}
