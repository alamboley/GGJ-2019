using UnityEngine;

public class HomeManager : MonoBehaviour
{
    public int life = 3;

    SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // continuing to solve this damn Unity bug
        foreach (Player player in FindObjectsOfType<Player>())
            player.OnTriggerExit2D(collision);

        //Destroy(collision.gameObject);
        collision.gameObject.SetActive(false);
        FindObjectOfType<WaveManager>().MonsterKilled(collision.GetComponent<Enemy>());

        if (--life == 0)
        {
            Game.instance.HomeIsDead();
            Destroy(this.gameObject);
        }

        sprite.color = new Color(1, 1, 1, (float)life / 6);
    }
}
