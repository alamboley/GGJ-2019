using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet BulletPrefab;

    public bool IsFiring { get; private set; }

    public float ShootingSpeed;

    float delay;

    void Start()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IsFiring = true;

        delay = 0;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        IsFiring = false;
    }

    void Update()
    {
        if (IsFiring)
        {
            if (delay >= ShootingSpeed)
            {
                Instantiate(BulletPrefab.gameObject, transform.position, transform.rotation);

                delay = 0;
            }

            delay += Time.deltaTime;
        }
    }
}
