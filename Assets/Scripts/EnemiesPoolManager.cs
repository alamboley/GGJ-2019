using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPoolManager : MonoBehaviour
{
    public Enemy EnemyPrefab;
    public int PoolSize = 50;

    List<Enemy> Enemies = new List<Enemy>();

    void Start()
    {
        for (int i = 0; i < PoolSize; ++i)
        {
            Enemy enemy = Instantiate(EnemyPrefab, transform);
            Enemies.Add(enemy);
            enemy.gameObject.SetActive(false);
        }
    }

    public Enemy GetFirstInactiveEnemy()
    {
        for (int i = 0; i < Enemies.Count; ++i)
            if (!Enemies[i].gameObject.activeInHierarchy)
                return Enemies[i];

        throw new Exception("pool too small");
    }
}
