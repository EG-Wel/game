using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public Vector3 spawn;
    public GameObject player;
    public GameObject[] heartArray;
    [SerializeField] private Canvas minimap;

    public int death = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Void") || collision.gameObject.CompareTag("Walrus"))
        {
            death++;
            for (int i = 0; i < death; i++)
            {
                Destroy(heartArray[i]);
                if (death == 3)
                {
                    player.gameObject.SetActive(false);
                    minimap.gameObject.SetActive(false);
                    GameOverScreen.Setup(FindObjectOfType<ScoreManager>().score);
                }
            }
            player.transform.SetPositionAndRotation(spawn, Quaternion.identity);
            GetComponent<PlayerMovement>().facingRight = true;
            FindObjectOfType<AudioManager>().Play("Death");
        }
    }
}
