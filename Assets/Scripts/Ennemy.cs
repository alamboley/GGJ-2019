using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public float speed = 2f;

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
