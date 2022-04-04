using System;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Window_QuestPointer wind;
    public Rigidbody2D playerRb;
    public GameObject menuScreen;
    public CharacterController2D controller;
    public Animator animator;
    public CircleCollider2D circleCollider;
    public Cinemachine.CinemachineVirtualCamera cam;
    public GameObject player;

    public Vector3 fishPositsion;
    public GameObject[] fishs;

    public Text tijd;

    float coyoteTime = 0.15f;
    public float coyoteTimeCounter;

    public float runSpeed = 40f;
    public bool isJumping = false;
    public bool facingRight = true;
    public bool menuActive = false;
    public float jumpTime;

    public float jumpCounter;
    float horizontalMove = 0f;
    public GameObject deur;

    public GameObject closest;
    public UnitySceneManager.Scene currentScene;

    private float LevelTime = 0f;
    Level level;

    private void Start()
    {
        currentScene = UnitySceneManager.SceneManager.GetActiveScene();
        sceneControll.instance.Scene(currentScene);
        closest = fishs[0]; level = LevelInfo.instance.levels[0];
    }

    void Update()
    {
        LevelTime += Time.deltaTime;
        LevelInfo.instance.levels[0].time = LevelTime;

        double mainGameTimerd = (double)level.time;
        TimeSpan time = TimeSpan.FromSeconds(mainGameTimerd);
        string displayTime = time.ToString("mm':'ss");

        tijd.text = displayTime;

        playerRb = GetComponent<Rigidbody2D>();

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        isJumping = !GetComponent<CharacterController2D>().m_Grounded;

        DraggUpdate(playerRb);
        CoyoteTime();

        if (Input.GetKeyDown(KeyCode.Escape))
            MenuScreen();

        HoldJump();
        Dive();
        Animatior();
        FishClose();
    }

    private void FixedUpdate() => controller.Move(horizontalMove * Time.deltaTime);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door"))
            CheckDoor(collision.gameObject);
    }

    void CheckDoor(GameObject collision)
    {
        if (collision.name == "Door_1-2")
        {
            if (ScoreManager.instance.score >= 4)
                Door("Congratulations1");
        }

        if (collision.name == "Door_2-3")
        {
            if (ScoreManager.instance.score >= 8)
                Door("Congratulations2");
        }

        if (collision.name == "Door_3-4")
        {
            if (ScoreManager.instance.score >= 4)
                Door("Congratulations3");
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
        if (menuActive)
        {
            player.SetActive(true);
            menuScreen.SetActive(false);
            menuActive = false;
            Time.timeScale = 1;
        }
        else
        {
            player.SetActive(false);
            Time.timeScale = 0;
            menuScreen.SetActive(true);
            menuActive = true;
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
        closest = fishs[0];
        foreach (GameObject fish in fishs)
        {
            if (Vector3.Distance(fish.transform.position, playerRb.transform.position) < Vector3.Distance(closest.transform.position, playerRb.transform.position))
            {
                if (!ScoreManager.instance.Enough(currentScene))
                    closest = fish;
                else
                    closest = deur;
            }
        }
        print(closest.transform.position);
        wind.Show(closest.transform.position);
    }
}