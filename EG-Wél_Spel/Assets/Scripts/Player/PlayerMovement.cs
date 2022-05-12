using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;


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
    public int totalFishs;
    
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
    public Scene currentScene;

    public float LevelTime = 0f;
    private Levels[] levels;
    public Levels level;

    private void Start()
    {
        levels = FindObjectOfType<LevelInfo>().levels;
        currentScene = SceneManager.GetActiveScene();
        sceneControll.instance.Scene(currentScene);
        LevelInfo.instance.currentScene = currentScene;
        closest = fishs[0]; 
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].sceneName == currentScene.name)
                levels[i].isCurrent = true;
            else
                levels[i].isCurrent = false;
            if (LevelInfo.instance.levels[i].isCurrent)
                level = LevelInfo.instance.levels[i];
        }
        FindObjectOfType<LevelInfo>().levels = levels;
        totalFishs = fishs.Length;
        FindObjectOfType<ScoreManager>().ChangeScore(0);
    }

    void Update()
    {
        double mainGameTimerd = (double)level.playTime;
        TimeSpan time = TimeSpan.FromSeconds(mainGameTimerd);
        string displayTime = time.ToString("mm':'ss");
        tijd.text = displayTime;

        playerRb = GetComponent<Rigidbody2D>();

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        isJumping = !GetComponent<CharacterController2D>().m_Grounded;

        if (Input.GetKeyDown(KeyCode.Escape))
            MenuScreen();

        DraggUpdate(playerRb);
        CoyoteTime();
        HoldJump();
        Dive();
        Animatior();
        FishClose();
        TimePerLevel();
    }

    private void TimePerLevel()
    {
        LevelTime += Time.deltaTime;
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].sceneName == currentScene.name)
                levels[i].playTime = LevelTime;
        }
    }

    private void FixedUpdate() => controller.Move(horizontalMove * Time.deltaTime);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Door")) 
            CheckDoor();
    }

    void CheckDoor()
    {
        if (totalFishs == FindObjectOfType<ScoreManager>().score)
        {
            SceneManager.LoadScene("Congratulations");
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
            playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y - 2f);
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
        if (fishs.Length == 0 && FindObjectOfType<ScoreManager>().score == totalFishs)
        {
            closest = deur;
        }
        else
        {
            closest = fishs[0];
            foreach (GameObject fish in fishs)
            {
                if (Vector3.Distance(fish.transform.position, playerRb.transform.position) < Vector3.Distance(closest.transform.position, playerRb.transform.position))
                {
                    if (fish.activeSelf)
                        closest = fish;
                }
            }
        }
        wind.Show(closest.transform.position);
    }
}