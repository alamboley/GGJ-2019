using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float LifeSpan;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        LifeSpan -= Time.deltaTime;
        if (LifeSpan < 0)
            Destroy(gameObject);
    }
}
