using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemyType type = EnemyType.W; 
    public float speed;
    public float life;
    public SpriteRenderer sr;

    float m_life;

    private void Awake()
    {
        if(sr == null)
            sr = GetComponentInChildren<SpriteRenderer>();
        
        m_life = life;
    }

    private void OnEnable()
    {
        sr.color = Game.instance.GetColor(this.type).color;
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
