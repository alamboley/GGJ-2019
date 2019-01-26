﻿using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float LifeSpan;
    public float Speed = 2;

    float x;
    float y;

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            return;

        Destroy(collision.gameObject);
        Destroy(gameObject);
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