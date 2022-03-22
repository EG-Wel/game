using UnityEngine;

public class Fish : MonoBehaviour
{
    public Fish fish;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(fish);
            FindObjectOfType<AudioManager>().Play("Fish");
            ScoreManager.instance.ChangeScore(1);
        }
    }
}
