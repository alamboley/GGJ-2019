using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public EnemyType type = EnemyType.W;
    public float LifeSpan;
    public float Speed = 2;
    public Player player { get; set; }

    float lifeSpan;

    public SpriteRenderer sr;

    private void Awake()
    {
        if (sr == null)
            sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        sr.color = Game.instance.GetColor(this.type).color;
    }


    void Start()
    {
        lifeSpan = LifeSpan;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Bullet")
            return;
            
        //FILTER BY TYPE
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy == null)
            return;

        if (enemy.type != this.type)
            return;


        gameObject.SetActive(false);

        if (--enemy.life == 0)
        {
            // uber ugly fix, damn Unity bug
            player.OnTriggerExit2D(collision);

            //Destroy(collision.gameObject);
            enemy.gameObject.SetActive(false);

            Game.instance.waveManager.MonsterKilled(enemy);
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
