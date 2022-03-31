using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class PlayerMovement : MonoBehaviour
{
    public Window_QuestPointer wind;
    public Rigidbody2D playerRb;
    public GameObject menuScreen;
    public CharacterController2D controller;
    public Animator animator;
    public CircleCollider2D circleCollider;
    public Cinemachine.CinemachineVirtualCamera cam;
    public GameObject deur;

    float coyoteTime = 0.15f;
    public float coyoteTimeCounter;

    public float runSpeed = 40f;
    public bool isJumping = false;
    public bool facingRight = true;
    public bool menuActive = false;
    public float jumpTime;

    public GameObject[] fishs;

    public float jumpCounter;
    float horizontalMove = 0f;
    private GameObject closest;

    private UnitySceneManager.Scene currentScene;

    private void Start()
    {
        currentScene = UnitySceneManager.SceneManager.GetActiveScene();
        //FindObjectOfType<sceneControll>().Scene(currentScene);
        sceneControll.instance.Scene(currentScene);
        closest = fishs[0];
    }

    void Update()
    {
        playerRb = GetComponent<Rigidbody2D>();

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        isJumping = !GetComponent<CharacterController2D>().m_Grounded;

        DraggUpdate(playerRb);
        CoyoteTime();
        MenuScreen();
        HoldJump();
        Dive();
        Animatior();
        FishClose();

        //wind.Show(new Vector3(0,0,0));
        if (Input.GetButtonDown("Jump"))
        {
            wind.targetPosition = new Vector3(25, 6, 0);
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime);
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
                Door("Level02");
        }

        if (collision.gameObject.name == "Door_2-3")
        {
            if (ScoreManager.instance.score >= 8)
                Door("Level03");
        }

        if (collision.gameObject.name == "Door_3-4")
        {
            if (ScoreManager.instance.score >= 4)
                Door("Level04");
        }

        void Door(string level)
        {
            UnitySceneManager.SceneManager.LoadScene(level);
            controller.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.identity);
            facingRight = true;
            FindObjectOfType<AudioManager>().Play("Door");
        }
    }

    // If player falling drag increases so it looks like he is gliding
    void DraggUpdate(Rigidbody2D rb)
    {
        
        if (rb.velocity.y < 0)
            rb.drag = 5f;
        else
            rb.drag = 0f;

        if (!isJumping)
            rb.drag = 0f;
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
            playerRb.drag = 0f;
    }

    // Alle animaties
    void Animatior()
    {
        // If user is holding down button player dives
        if (Input.GetButton("Down") && isJumping)
            animator.SetBool("IsDiving", true);

        if (Input.GetButtonUp("Down"))
            animator.SetBool("IsDiving", false);

        // Wanneer player jumped show jump animation
        if (FindObjectOfType<CharacterController2D>().m_Grounded)
            animator.SetBool("IsJumping", false);
        else
            animator.SetBool("IsJumping", true);

        // If player is moving -> play running animation
        // Else stop animation
        if (horizontalMove != 0)
            animator.SetFloat("IsRunning", 1f);
        else
            animator.SetFloat("IsRunning", 0f);

        if (horizontalMove > 0)
            animator.SetBool("FacingRight", true);
        if (horizontalMove < 0)
            animator.SetBool("FacingRight", false);
    }

    private void FishClose()
    {
        foreach (GameObject fish in fishs)
        {
            if (Vector3.Distance(fish.transform.position, playerRb.transform.position) < Vector3.Distance(closest.transform.position, playerRb.transform.position))
            {
                if (ScoreManager.instance.Enough(currentScene))
                    closest = deur;
                else
                    closest = fish;
            }
        }
        print(closest);
        wind.Show(closest.transform.position);
    }
}