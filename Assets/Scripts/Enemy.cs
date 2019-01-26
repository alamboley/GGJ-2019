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
        transform.LookAt(Vector3.zero);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        transform.rotation = Quaternion.identity;
    }
}
