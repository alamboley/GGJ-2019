using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public Text LifeText; 
    public int life = 3;

    void Start()
    {
        UpdateLifeText();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // continuing to solve this damn Unity bug
        foreach (Player player in FindObjectsOfType<Player>())
            player.OnTriggerExit2D(collision);

        Destroy(collision.gameObject);
        FindObjectOfType<WaveManager>().MonsterKilled(collision.GetComponent<Enemy>());

        if (--life == 0)
        {
            Game.instance.HomeIsDead();
            Destroy(this.gameObject);
            return;
        }

        UpdateLifeText();
    }
    
    void UpdateLifeText()
    {
        LifeText.text = "Vies : " + life;
    }
}
