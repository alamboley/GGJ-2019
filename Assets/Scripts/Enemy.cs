using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float life;

    float m_life;

    void Awake()
    {
        m_life = life;
    }
    
    void Update()
    {
        transform.position += -transform.position.normalized * Time.deltaTime * speed;
    }

    public void ResetEnemy()
    {
        life = m_life;
    }
}
