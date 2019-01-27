using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemyType type = EnemyType.W; 
    public float speed;
    public float life;

    public SpriteRenderer sr;
    private void Awake()
    {
        if(sr == null)
            sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        sr.color = Game.instance.GetColor(this.type).color;
    }

    void Update()
    {
        transform.position += -transform.position.normalized * Time.deltaTime * speed;
    }
}
