using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int currentWave = 1;
    public int monsterCount = 1;
    public int waveDuration = 10;
    public float ratioSpawn = 1.9f;

    public Enemy EnemyPrefab;
    public Transform RootSpawner;

    float time = 0;
    float diagonal;


    void Start()
    {
        currentWave = 1;

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        diagonal = Mathf.Sqrt((height * height) + (width * width)) / 2;
    }
    
    void Update()
    {
        if(Time.time - time > waveDuration)
        {
            currentWave++;
            time = Time.time;
            SpawnMonsters();
        }
    }

    void SpawnMonsters()
    {
        Instantiate(EnemyPrefab, GetRandomPosition(), Quaternion.identity, RootSpawner);
    }

    Vector3 GetRandomPosition()
    {
        Vector2 randomAngle = Random.insideUnitCircle.normalized;

        return randomAngle * diagonal;
    }
}
