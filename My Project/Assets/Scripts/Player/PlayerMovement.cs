using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
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
    public bool facingRight = true;
    public bool floor = false;
    public bool menuActive = false;
    public bool movingRight;
    public bool movingLeft;
    public float jumpTime; 

    private bool isGrounded;
    public float jumpCounter;
    float horizontalMove = 0f;
    
    private void Start()
    {
        UnitySceneManager.Scene currentScene = UnitySceneManager.SceneManager.GetActiveScene();
        FindObjectOfType<sceneControll>().Scene(currentScene);
    }

    void Update()
    {
        playerRb = GetComponent<Rigidbody2D>();

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
        isGrounded = GetComponent<CharacterController2D>().m_Grounded;

        DraggUpdate(playerRb);

        SetIsJumping();

        CoyoteTime();

        WalkingAnim();

        JumpingAnim();

        MenuScreen();

        HoldJump();
        
        flip();

        if (Input.GetButton("Down") && isJumping)
        {
            playerRb.drag = 0f;
            animator.SetBool("IsDiving", true);
        }
        if (Input.GetButtonUp("Down"))
        {
            animator.SetBool("IsDiving", false);
        }
        
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Void"))
            collision.gameObject.GetComponent<Canvas>().enabled = true;
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
        if (horizontalMove > 0)
            gameObject.transform.Rotate(0f, 180f, 0f);
        else if (horizontalMove < 0)
            gameObject.transform.Rotate(0f, 0f, 0f);
    }

    void DraggUpdate(Rigidbody2D rb)
    {
        // When player falling drag increases so it looks like he is gliding
        if (rb.velocity.y < 0)
            rb.drag = 5f;
        else
            rb.drag = 0f;

        if (isGrounded)
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
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpCounter = jumpTime;
            playerRb.velocity = new Vector2(playerRb.velocity.x, GetComponent<CharacterController2D>().m_JumpForce);
            //playerRb.velocity = Vector2.up * GetComponent<CharacterController2D>().m_JumpForce;
        }
        
        jumpCounter -= Time.deltaTime;

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpCounter > 0)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, GetComponent<CharacterController2D>().m_JumpForce);
            }
            else
                isJumping = false;
        }

        if (Input.GetButtonUp("Jump"))
            isJumping = false;
    }
}
