using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public int life = 3;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // continuing to solve this damn Unity bug
        foreach (Player player in FindObjectsOfType<Player>())
            player.OnTriggerExit2D(collision);

        Destroy(collision.gameObject);

        if (life-- == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
