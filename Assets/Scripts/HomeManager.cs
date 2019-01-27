using UnityEngine;
using System.Collections.Generic;

public class HomeManager : MonoBehaviour
{
    public List<SpriteRenderer> lifeSprites;

    int maxLife = 6;
    int life = 0;


    void Start()
    {
        life = maxLife;
        UpdateLifeDisplay(6);
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

        UpdateLifeDisplay(life);
    }

    void UpdateLifeDisplay(int l)
    {
        foreach (SpriteRenderer sr in lifeSprites)
            sr.gameObject.SetActive(false);

        if(l > 0)
        {
            lifeSprites[l - 1].gameObject.SetActive(true);
        }

    }
}
