using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPoolManager : MonoBehaviour
{
    public Bullet BulletPrefab;
    public int PoolSize = 50;

    List<Bullet> Bullets = new List<Bullet>();

    void Start()
    {
        for (int i = 0; i < PoolSize; ++i)
        {
            Bullet bullet = Instantiate(BulletPrefab, transform);
            Bullets.Add(bullet);
            bullet.gameObject.SetActive(false);
        }
    }

    public Bullet GetFirstInactiveBullet()
    {
        for (int i = 0; i < Bullets.Count; ++i)
            if (!Bullets[i].gameObject.activeInHierarchy)
                return Bullets[i];

        throw new Exception("pool too small");
    }
}
