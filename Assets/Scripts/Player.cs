﻿using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public Bullet BulletPrefab;

    public Gradient Color;

    public float AimSpeed;
    public float ShootingSpeed;

    bool hasAimed;
    float delay;

    public List<Collider2D> enemiesInRange;

    SpriteRenderer sprite;

    private void Awake()
    {
        enemiesInRange = new List<Collider2D>();

        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Bullet" && !enemiesInRange.Contains(collision))
        {
            enemiesInRange.Add(collision);

            delay = 0;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (enemiesInRange.Contains(collision))
            enemiesInRange.Remove(collision);
    }

    void Update()
    {
        if (enemiesInRange.Count > 0)
        {
            if (!hasAimed)
            {
                sprite.color = Color.Evaluate(delay);

                if (delay >= AimSpeed)
                {
                    Fire();

                    hasAimed = true;
                }
            }
            else if (delay >= ShootingSpeed)
                Fire();

             delay += Time.deltaTime;
        }
        else
        {
            sprite.color = Color.Evaluate(0);

            hasAimed = false;
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(BulletPrefab.gameObject, transform.position, transform.rotation);
        bullet.GetComponent<Bullet>().player = this;

        delay = 0;
    }
}
