using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float LifeSpan;
    public float Speed = 2;
    public Player player { get; set; }

    float lifeSpan;

    void Start()
    {
        lifeSpan = LifeSpan;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Bullet")
            return;

        gameObject.SetActive(false);

        Enemy enemy = collision.GetComponent<Enemy>();

        if (--enemy.life == 0)
        {
            // uber ugly fix, damn Unity bug
            player.OnTriggerExit2D(collision);

            //Destroy(collision.gameObject);
            enemy.gameObject.SetActive(false);

            FindObjectOfType<WaveManager>().MonsterKilled(enemy);
        }
    }

    void Update()
    {
        float x = Mathf.Cos((transform.eulerAngles.z + 90) * Mathf.Deg2Rad);
        float y = Mathf.Sin((transform.eulerAngles.z + 90) * Mathf.Deg2Rad);
        transform.position += new Vector3(x, y, 0) * Speed * Time.deltaTime;

        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0)
            gameObject.SetActive(false);
    }

    public void ResetBullet()
    {
        lifeSpan = LifeSpan;
    }
}
