﻿using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enemy spotted");
    }

    void Update()
    {
        
    }
}
