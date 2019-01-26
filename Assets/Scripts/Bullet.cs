using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float LifeSpan;
    public float Speed = 2;
    public Player player { get; set; }

    float x;
    float y;

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Bullet")
            return;

        Destroy(gameObject);

        Enemy enemy = collision.GetComponent<Enemy>();

        if (--enemy.life == 0)
        {
            // uber ugly fix, damn Unity bug
            player.OnTriggerExit2D(collision);

            Destroy(collision.gameObject);

            FindObjectOfType<WaveManager>().MonsterKilled(enemy);
        }
    }

    void Update()
    {
        x = Mathf.Cos((transform.eulerAngles.z + 90) * Mathf.Deg2Rad);
        y = Mathf.Sin((transform.eulerAngles.z + 90) * Mathf.Deg2Rad);
        transform.position += new Vector3(x, y, 0) * Speed * Time.deltaTime;

        LifeSpan -= Time.deltaTime;
        if (LifeSpan < 0)
            Destroy(gameObject);
    }
}
