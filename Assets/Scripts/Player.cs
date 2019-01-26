using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public Bullet BulletPrefab;

    public float ShootingSpeed;

    float delay;

    public List<Collider2D> enemiesInRange;

    private void Awake()
    {
        enemiesInRange = new List<Collider2D>();
    }

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Bullet" && !enemiesInRange.Contains(collision))
            enemiesInRange.Add(collision);

        delay = 0;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (enemiesInRange.Contains(collision))
            enemiesInRange.Remove(collision);
    }

    void Update()
    {
        if (enemiesInRange.Count > 0)
        {
            if (delay >= ShootingSpeed)
            {
                Instantiate(BulletPrefab.gameObject, transform.position, transform.rotation);

                delay = 0;
            }

            delay += Time.deltaTime;
        }
    }
}
