﻿using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public float AimSpeed;
    public float ShootingSpeed;

    BulletsPoolManager BulletsPoolManager;
    WaveManager WaveManager;

    bool hasAimed;
    float delay;

    public List<Collider2D> enemiesInRange = new List<Collider2D>();

    SpriteRenderer sprite;

    private void Awake()
    {
        BulletsPoolManager = FindObjectOfType<BulletsPoolManager>();
        WaveManager = FindObjectOfType<WaveManager>();

        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !enemiesInRange.Contains(collision))
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
            hasAimed = false;
        }
    }

    void Fire()
    {
        Bullet bullet = BulletsPoolManager.GetFirstInactiveBullet();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.player = this;

        bullet.ResetBullet();

        bullet.gameObject.SetActive(true);

        delay = 0;
    }
}
