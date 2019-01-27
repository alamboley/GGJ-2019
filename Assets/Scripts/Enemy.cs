using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float life;

    void Start()
    {
        
    }
    
    void Update()
    {
        transform.position += -transform.position.normalized * Time.deltaTime * speed;
    }
}
